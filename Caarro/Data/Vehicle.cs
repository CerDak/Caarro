namespace Caarro.Data;

public class Vehicle : BaseEntity
{
    public override int Id { get; set; }
    public override bool Active { get; set; }
    public override DateTime Date { get; set; }
    public string Name { get; set; } = null!;
    public string? Manufacturer { get; set; }
    public string? Model { get; set; }
    public string? LicensePlate { get; set; }
    public int? Year { get; set; }
    public string? VehicleIdentificationNumber { get; set; }
    public double? FuelCapacity { get; set; }
    public FuelType FuelType { get; set; }
    public UnitOfMeasure UnitOfMeasure { get; set; }

    public ICollection<Reminder>? Reminders { get; set; }
    public ICollection<Service>? Services { get; set; }
    public ICollection<Refueling>? Refuelings { get; set; }
    public ICollection<Expense>? Expenses { get; set; }
    public ICollection<Income>? Income { get; set; }
}

public enum UnitOfMeasure
{
    Mile,
    Kilometer,
}

public enum FuelType
{
    Gas,
    Diesel,
    Electric,
    GasMidgrade,
    GasPremium,
    BioDiesel,
    Ethanol,
    CNG,
    LPG,
}
