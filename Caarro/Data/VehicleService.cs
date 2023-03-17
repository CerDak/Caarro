using Microsoft.EntityFrameworkCore;

namespace Caarro.Data;

public class VehicleService
{
    private ILogger<VehicleService> _log;
    private readonly CaarroDbContext _db;

    public VehicleService(ILogger<VehicleService> logger, CaarroDbContext db)
    {
        _log = logger;
        _db = db;
    }
    
    public async Task<List<Vehicle>> GetAllVehicles() => await _db.Vehicles.ToListAsync();

    public async Task AddVehicle(Vehicle car)
    {
        _db.Vehicles.Add(car);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteVehicle(Vehicle car)
    {
        _db.Vehicles.Remove(car);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateVehicle(Vehicle car)
    {
        _db.Vehicles.Update(car);
        await _db.SaveChangesAsync();
    }
}