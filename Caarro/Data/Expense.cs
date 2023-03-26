namespace Caarro.Data;

public class Expense : BaseEntity
{
    public override int Id { get; set; }
    public override bool Active { get; set; }

    public override DateTime Date { get; set; }
    public uint Odometer { get; set; }
    public string Name { get; set; } = null!;
    public string? Location { get; set; }
    public string? Driver { get; set; }
    public string? PaymentMethod { get; set; }
    public string? Reason { get; set; }
    public double Amount { get; set; }

    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
}
