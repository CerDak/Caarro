namespace Caarro.Data;

public abstract class BaseEntity
{
    public abstract int Id { get; set; }
    public abstract string PartitionKey { get; set; }
    public abstract DateTime Date { get; set; }
    public abstract TimeZoneInfo Timezone { get; set; }
}