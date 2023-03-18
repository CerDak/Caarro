using Caarro.Data;
using Microsoft.EntityFrameworkCore;

namespace Caarro.Service;

public class VehicleService
{
    private ILogger<VehicleService> _log;
    private readonly CaarroDbContext _db;

    public VehicleService(ILogger<VehicleService> logger, CaarroDbContext db)
    {
        _log = logger;
        _db = db;
    }

    public async Task<List<Vehicle>> GetAllVehicles()
    {
        return await _db.Vehicles.Where(v => v.Active == true).ToListAsync();
    }

    public async Task<Vehicle> GetVehicle(int id)
    {
        return await _db.Vehicles.SingleOrDefaultAsync(v => v.Id == id);
    }

    public async Task AddVehicle(Vehicle vehicle)
    {
        vehicle.Date = DateTime.Now;
        vehicle.Active = true;

        _db.Vehicles.Add(vehicle);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteVehicle(Vehicle vehicle)
    {
        var car = await _db.Vehicles.SingleOrDefaultAsync(v => v.Id == vehicle.Id);
        if (car is not null)
        {
            car.Active = false;
            await _db.SaveChangesAsync();
        }
    }

    public async Task UpdateVehicle(Vehicle vehicle)
    {
        _db.Vehicles.Update(vehicle);
        await _db.SaveChangesAsync();
    }
}