namespace Caarro.Data;

public class Income : BaseEntity
{
    public override int Id { get; set; }
    public override DateTime Date { get; set; }
    public uint Odometer { get; set; }
    public string Name { get; set; } = null!;
    public double Amount { get; set; }
    public string? Driver { get; set; }
    public string? Location { get; set; }
}