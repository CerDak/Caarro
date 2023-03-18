using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Caarro.Data;

public class CaarroDbContext : DbContext
{
    public CaarroDbContext(DbContextOptions<CaarroDbContext> options) : base(options) { }

    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Refueling> Refueling { get; set; }
    public DbSet<Reminder> Reminders { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Income> Income { get; set; }
    public DbSet<Expense> Expenses { get; set; }
}

internal class UtcValueConverter : ValueConverter<DateTime, DateTime>
{
    public UtcValueConverter() : base(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc)) { }
}