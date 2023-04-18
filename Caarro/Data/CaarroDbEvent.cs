// Copyright © CerDak

using Caarro.Services;

using EntityFrameworkCore.Triggered;

namespace Caarro.Data;

public class SendDistanceNotification : IAfterSaveTrigger<Refueling>
{
    private readonly ReminderService _reminderService;
    private readonly VehicleService _vehicleService;
    private readonly ILogger<SendDistanceNotification> _log;

    public SendDistanceNotification(ReminderService reminderService, VehicleService vehicleService, ILogger<SendDistanceNotification> log)
    {
        _reminderService = reminderService;
        _vehicleService = vehicleService;
        _log = log;
    }
    
    public async Task AfterSave(ITriggerContext<Refueling> context, CancellationToken cancellationToken)
    {
        var triggeredReminders = await _reminderService.GetReminderUnderDistanceAsync(context.Entity.VehicleId, context.Entity.Odometer);
        
        // This could become quadratic :/
        foreach (var reminder in triggeredReminders)
        {
            var vehicle = await _vehicleService.GetVehicleAsync(reminder.VehicleId);

            _log.LogCritical("ATTN: {email} - Vehicle {vehicle} / {name} upcoming service: {service}",
                reminder.ContactEmail, reminder.VehicleId, vehicle!.Name, reminder.Service.ToString());
            
            await _reminderService.DeleteReminderAsync(reminder);
        }
    }
}
