using Microsoft.EntityFrameworkCore;

namespace Caarro.Data;

public class CaarroDbContext : DbContext
{
    public CaarroDbContext(DbContextOptions<CaarroDbContext> options) : base(options) { }
    
    public DbSet<Vehicle> Vehicles { get; set; }
}