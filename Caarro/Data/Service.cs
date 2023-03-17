namespace Caarro.Data;

public class Service : BaseEntity
{
    public override int Id { get; set; }
    public override string PartitionKey { get; set; }
    public override DateTime Date { get; set; }
    public override TimeZoneInfo Timezone { get; set; }
    public uint Odometer { get; set; }
    public ServiceType Type { get; set; }
    public string? Location { get; set; }
    public string? Driver { get; set; }
    public string? PaymentMethod { get; set; }
    public decimal Amount { get; set; }
}

[Flags]
public enum ServiceType
{
    AirConditioning,
    AirFilter,
    Battery,
    Belts,
    Body,
    BreakPad,
    BreakFluid,
    BreakReplacement,
    CabinAirFilter,
    ClutchFluid,
    ClutchSystem,
    CoolingSystem,
    EngineRepair,
    ExhaustSystem,
    FuelFilter,
    FuelPump,
    Glass,
    Mirror,
    HeatingSystem,
    Horn,
    Inspection,
    Lights,
    Tires,
    TireRotation,
    OilChange,
    OilFilter,
    Radiator,
    RearDifferential,
    SeatBelt,
    SparkPlugs,
    Steering,
    Suspension,
    TransferCase,
    Transmission,
    WheelAlignment,
    WindshieldWipers,
}