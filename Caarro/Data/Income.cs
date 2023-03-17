namespace Caarro.Data;

public class Income : BaseEntity
{
    public override int Id { get; set; }
    public override string PartitionKey { get; set; }
    public override DateTime Date { get; set; }
    public override TimeZoneInfo Timezone { get; set; }
    public uint Odometer { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public string? Driver { get; set; }
    public string? Location { get; set; }
}