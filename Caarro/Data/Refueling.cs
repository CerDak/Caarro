namespace Caarro.Data;

public class Refueling : BaseEntity
{
    public override int Id { get; set; }
    public override string PartitionKey { get; set; }
    public override DateTime Date { get; set; }
    public override TimeZoneInfo Timezone { get; set; }
    public uint Odometer { get; set; }
    public string Fuel { get; set; }
    public decimal Price { get; set; }
    public decimal FuelAmount { get; set; }
    public bool ToFull { get; set; }
    public string?  Location { get; set; }
    public string? Driver { get; set; }
}