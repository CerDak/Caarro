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

    public async Task<IEnumerable<Reminder>> GetAllRemindersAsync(int carId)
    {
        return await _db.Reminders.Where(r => r.VehicleId == carId && r.Active == true).ToListAsync();
    }

    public async Task<Reminder?> GetReminderAsync(int id)
    {
        return await _db.Reminders.SingleOrDefaultAsync(r => r.Id == id);
    }

    public async Task AddReminderAsync(Reminder reminder)
    {
        await using var transaction = await _db.Database.BeginTransactionAsync();

        var scheduler = await _scheduler.GetScheduler();

        var guid = Guid.NewGuid();
        reminder.Date = DateTime.Now;
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
            await _db.SaveChangesAsync();

            var job = JobBuilder.Create<SendNotificationJob>()
                .UsingJobData("reminderId", entry.Entity.Id)
                .WithIdentity(jobKey)
                .StoreDurably()
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .StartAt(date)
                .Build();

            await scheduler.ScheduleJob(job, trigger);

            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            //var jobKey = new JobKey(guid.ToString(), reminder.VehicleId.ToString());
            var deleteJob = await scheduler.DeleteJob(jobKey);

            if (deleteJob is false)
            {
                _logger.LogCritical("Failed to delete job {jobId} on {vehicleId}",
                    guid, reminder.VehicleId);
            }

            await transaction.RollbackAsync();

            throw;
        }
    }

    public async Task DeleteReminderAsync(Reminder reminder)
    {
        var x = await _db.Reminders.SingleOrDefaultAsync(r => r.Id == reminder.Id);
        if (x is not null)
        {
            await using var transaction = await _db.Database.BeginTransactionAsync();
            var scheduler = await _scheduler.GetScheduler();
            JobKey jobKey = new(reminder.JobId.ToString(), reminder.VehicleId.ToString());

            try
            {
                _db.Reminders.Remove(x);
                await scheduler.DeleteJob(jobKey);
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
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

    public async Task UpdateReminderAsync(Reminder reminder)
    {
        _db.Reminders.Update(reminder);
        await _db.SaveChangesAsync();
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

        var reminder = await _reminderService.GetReminderAsync(reminderId);

        if (reminder is null) { throw new Exception("This shouldn't happen"); }

        var vehicle = await _vehicleService.GetVehicleAsync(reminder.VehicleId);

        if (vehicle is null) { throw new Exception("Vehicle no longer exists"); }

        //Send the mail
        _logger.LogCritical("ATTN: {email} - Vehicle {vehicle} / {name} upcoming service: {service}",
            reminder.ContactEmail, reminder.VehicleId, vehicle.Name, reminder.Service.ToString());
    }
}
