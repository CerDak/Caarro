namespace Caarro.Data;

public class Reminder : BaseEntity
{
    public override int Id { get; set; }
    public override bool Active { get; set; }
    public override DateTime Date { get; set; }
    public TimeSpan? ReminderPeriodTime { get; set; }
    public int? ReminderPeriodDistance { get; set; }
    public ServiceType Service { get; set; }
    
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
}