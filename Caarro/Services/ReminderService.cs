// Copyright © CerDak

using Caarro.Data;

using Microsoft.EntityFrameworkCore;

using NodaTime;

using Quartz;

namespace Caarro.Services;

public class ReminderService
{
    private readonly ILogger<ReminderService> _logger;
    private readonly CaarroDbContext _db;
    private readonly ISchedulerFactory _scheduler;

    public ReminderService(ILogger<ReminderService> logger, CaarroDbContext db, ISchedulerFactory scheduler)
    {
        _logger = logger;
        _db = db;
        _scheduler = scheduler;
    }

    public async Task<IEnumerable<Reminder>> GetAllRemindersAsync(int carId, CancellationToken ct)
    {
        return await _db.Reminders.Where(r => r.VehicleId == carId && r.Active == true).ToListAsync(ct);
    }

    public async Task<Reminder?> GetReminderAsync(int id, CancellationToken ct)
    {
        return await _db.Reminders.SingleOrDefaultAsync(r => r.Id == id, ct);
    }

    public async Task<IEnumerable<Reminder>> GetReminderUnderDistanceAsync(int vehicleId, int distance, CancellationToken ct)
    {
        return await _db.Reminders
            .Where(r => r.VehicleId == vehicleId && distance >= r.ReminderPeriodDistance)
            .ToListAsync(ct);
    }

    public async Task AddReminderAsync(Reminder reminder, CancellationToken ct)
    {
        switch (reminder)
        {
            case { ReminderPeriodDistance: not null, ReminderPeriodTime: null }:
                await PersistDistanceReminder(reminder, ct);
                return;
            case { ReminderPeriodDistance: null, ReminderPeriodTime: not null }:
                await PersistTimeReminder(reminder, ct);
                return;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private async Task PersistTimeReminder(Reminder reminder, CancellationToken ct)
    {
        await using var transaction = await _db.Database.BeginTransactionAsync(ct);

        var scheduler = await _scheduler.GetScheduler(ct);

        var guid = Guid.NewGuid();
        reminder.Date = DateTime.UtcNow;
        reminder.Active = true;
        reminder.JobId = guid;

        JobKey jobKey = new(guid.ToString(), reminder.VehicleId.ToString());
        TriggerKey triggerKey = new(guid.ToString(), reminder.VehicleId.ToString());

        try
        {
            var timeZone = DateTimeZoneProviders.Tzdb["America/Los_Angeles"];
            var now = SystemClock.Instance.GetCurrentInstant();
            var dur = Duration.FromSeconds(reminder.ReminderPeriodTime!.Value.TotalSeconds);
            var date = now.Plus(dur).InZone(timeZone).ToDateTimeOffset();

            var entry = _db.Reminders.Add(reminder);
            await _db.SaveChangesAsync(ct);

            var job = JobBuilder.Create<SendNotificationJob>()
                .UsingJobData("reminderId", entry.Entity.Id)
                .WithIdentity(jobKey)
                .StoreDurably()
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .StartAt(date)
                .Build();

            await scheduler.ScheduleJob(job, trigger, ct);

            await transaction.CommitAsync(ct);
        }
        catch (Exception e)
        {
            var jobDeleted = await scheduler.DeleteJob(jobKey);

            if (jobDeleted is false)
            {
                _logger.LogCritical("Failed to delete job {jobId} on {vehicleId}",
                    guid, reminder.VehicleId);
            }

            await transaction.RollbackAsync();

            throw;
        }
    }

    private async Task PersistDistanceReminder(Reminder reminder, CancellationToken ct)
    {
        reminder.Date = DateTime.UtcNow;
        reminder.Active = true;
        reminder.JobId = Guid.Empty;

        _db.Reminders.Add(reminder);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteReminderAsync(Reminder reminder, CancellationToken ct)
    {
        var x = await _db.Reminders.SingleOrDefaultAsync(r => r.Id == reminder.Id, ct);
        if (x is not null)
        {
            await using var transaction = await _db.Database.BeginTransactionAsync(ct);
            var scheduler = await _scheduler.GetScheduler(ct);
            JobKey jobKey = new(reminder.JobId.ToString(), reminder.VehicleId.ToString());

            try
            {
                _db.Reminders.Remove(x);
                await scheduler.DeleteJob(jobKey, ct);
                await _db.SaveChangesAsync(ct);
                await transaction.CommitAsync(ct);
            }
            catch (Exception e)
            {
                _logger.LogCritical("Failed to delete job {jobId} on {vehicleId}",
                    jobKey.Name, reminder.VehicleId);

                await transaction.RollbackAsync();

                throw;
            }
        }
    }

    public async Task UpdateReminderAsync(Reminder reminder, CancellationToken ct)
    {
        _db.Reminders.Update(reminder);
        await _db.SaveChangesAsync(ct);
    }
}

internal class SendNotificationJob : IJob
{
    private readonly ILogger<SendNotificationJob> _logger;
    private readonly ReminderService _reminderService;
    private readonly VehicleService _vehicleService;

    public SendNotificationJob(ILogger<SendNotificationJob> logger, ReminderService reminderService, VehicleService vehicleService)
    {
        _logger = logger;
        _reminderService = reminderService;
        _vehicleService = vehicleService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        context.MergedJobDataMap.TryGetIntValue("reminderId", out int reminderId);

        var reminder = await _reminderService.GetReminderAsync(reminderId, default);

        if (reminder is null) { throw new Exception("This shouldn't happen"); }

        var vehicle = await _vehicleService.GetVehicleAsync(reminder.VehicleId, default);

        if (vehicle is null) { throw new Exception("Vehicle no longer exists"); }

        //Send the mail
        _logger.LogCritical("ATTN: {email} - Vehicle {vehicle} / {name} upcoming service: {service}",
            reminder.ContactEmail, reminder.VehicleId, vehicle.Name, reminder.Service.ToString());
    }
}
