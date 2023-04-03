using Caarro.Data;

using Microsoft.EntityFrameworkCore;

namespace Caarro.Services;

public class VehicleService
{
    private readonly ILogger<VehicleService> _log;
    private readonly CaarroDbContext _db;

    public VehicleService(ILogger<VehicleService> logger, CaarroDbContext db)
    {
        _log = logger;
        _db = db;
    }

    public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
    {
        return await _db.Vehicles.Where(v => v.Active == true).ToListAsync();
    }

    public async Task<Vehicle?> GetVehicleAsync(int id)
    {
        return await _db.Vehicles.SingleOrDefaultAsync(v => v.Id == id);
    }

    public async Task<int> AddVehicleAsync(Vehicle vehicle)
    {
        vehicle.Date = DateTime.Now;
        vehicle.Active = true;

        var e = _db.Vehicles.Add(vehicle);
        await _db.SaveChangesAsync();
        return e.Entity.Id;
    }

    public async Task DeleteVehicleAsync(Vehicle vehicle)
    {
        var car = await _db.Vehicles.SingleOrDefaultAsync(v => v.Id == vehicle.Id);
        if (car is not null)
        {
            car.Active = false;
            await _db.SaveChangesAsync();
        }
    }

    public async Task UpdateVehicleAsync(Vehicle vehicle)
    {
        _db.Vehicles.Update(vehicle);
        await _db.SaveChangesAsync();
    }
}
