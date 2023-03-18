namespace Caarro.Data;

public class Refueling : BaseEntity
{
    public override int Id { get; set; }
    public override DateTime Date { get; set; }
    public uint Odometer { get; set; }
    public string Fuel { get; set; }
    public double Price { get; set; }
    public double FuelAmount { get; set; }
    public bool ToFull { get; set; }
    public string? Location { get; set; }
    public string? Driver { get; set; }
}