using Microsoft.EntityFrameworkCore;

namespace Caarro.Data;

public class CaarroDbContext : DbContext
{
    public CaarroDbContext(DbContextOptions<CaarroDbContext> options) : base(options) { }
    
    public DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultContainer("vehicles");
        modelBuilder.Entity<Vehicle>()
            .ToContainer("vehicles");
    }
}