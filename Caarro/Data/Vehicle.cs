namespace Caarro.Data;

public class Vehicle : BaseEntity
{
    public override int Id { get; set; }
    public override string PartitionKey { get; set; }
    public override DateTime Date { get; set; }
    public override TimeZoneInfo Timezone { get; set; }
    public string Name { get; set; }
    public string? Manufacturer { get; set; }
    public string? Model { get; set; }
    public string? LicensePlate { get; set; }
    public uint Year { get; set; }
    public string? VehicleIdentificationNumber { get; set; }
    public bool Active { get; set; }
    public decimal FuelCapacity { get; set; }
    public string? FuelType { get; set; }
    public UnitOfMeasure UnitOfMeasure { get; set; }
}

public enum UnitOfMeasure
{
    Mile = 1,
    Kilometer = 2,
}
