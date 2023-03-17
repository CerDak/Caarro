namespace Caarro.Data;

public class Vehicle
{
    public int Id { get; set; }
    public string ParitionKey { get; set; }
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
    public uint Distence { get; set; }
}

public enum UnitOfMeasure
{
    Mile = 1,
    Kilometer = 2,
}
