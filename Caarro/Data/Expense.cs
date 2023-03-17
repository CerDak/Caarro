namespace Caarro.Data;

public class Expense : BaseEntity
{
    public override int Id { get; set; }

    public override DateTime Date { get; set; }
    public override TimeZoneInfo Timezone { get; set; }
    public uint Odometer { get; set; }
    public string Name { get; set; }
    public string? Location { get; set; }
    public string? Driver { get; set; }
    public string? PaymentMethod { get; set; }
    public string? Reason { get; set; }
    public decimal Amount { get; set; }
}