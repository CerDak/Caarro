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

    public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync(CancellationToken ct)
    {
        return await _db.Vehicles.Where(v => v.Active == true).ToListAsync(ct);
    }

    public async Task<Vehicle?> GetVehicleAsync(int id, CancellationToken ct)
    {
        return await _db.Vehicles.SingleOrDefaultAsync(v => v.Id == id, ct);
    }

    public async Task AddVehicleAsync(Vehicle vehicle, CancellationToken ct)
    {
        vehicle.Date = DateTime.Now;
        vehicle.Active = true;
        vehicle.AverageFuelEconomy = 0;

        _db.Vehicles.Add(vehicle);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteVehicleAsync(Vehicle vehicle, CancellationToken ct)
    {
        var car = await _db.Vehicles.SingleOrDefaultAsync(v => v.Id == vehicle.Id, ct);
        if (car is not null)
        {
            car.Active = false;
            await _db.SaveChangesAsync(ct);
        }
    }

    public async Task UpdateVehicleAsync(Vehicle vehicle, CancellationToken ct)
    {
        _db.Vehicles.Update(vehicle);
        await _db.SaveChangesAsync(ct);
    }
}
