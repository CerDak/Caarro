namespace Caarro.Data;

public abstract class BaseEntity
{
    public abstract int Id { get; set; }
    public abstract bool Active { get; set; }
    public abstract DateTime Date { get; set; }
}
