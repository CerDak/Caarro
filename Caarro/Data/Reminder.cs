namespace Caarro.Data;

public class Reminder : BaseEntity
{
    public override int Id { get; set; }
    public override DateTime Date { get; set; }
    public TimeSpan? ReminderPeriodTime { get; set; }
    public uint ReminderPeriodDistance { get; set; }
    public ServiceType Service { get; set; }
}