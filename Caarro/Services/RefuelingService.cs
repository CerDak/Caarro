using Caarro.Data;

using Microsoft.EntityFrameworkCore;

namespace Caarro.Services;

public class RefuelingService
{
    private readonly ILogger<RefuelingService> _log;
    private readonly CaarroDbContext _db;

    public RefuelingService(ILogger<RefuelingService> logger, CaarroDbContext db)
    {
        _log = logger;
        _db = db;
    }

    public async Task<IEnumerable<Refueling>> GetAllRefuelingsAsync(int carId)
    {
        return (await _db.Refueling.Where(r => r.VehicleId == carId && r.Active == true).ToListAsync())!;
    }

    public async Task<Refueling?> GetRefuelingAsync(int id)
    {
        return await _db.Refueling.SingleOrDefaultAsync(v => v.Id == id);
    }

    public async Task AddRefuelingAsync(Refueling refueling)
    {
        var old = await _db.Refueling
            .FromSqlInterpolated(
                $"SELECT * FROM Refueling WHERE VehicleId = {refueling.VehicleId} ORDER BY ID DESC LIMIT 1")
            .SingleOrDefaultAsync();
        if (old is not null)
        {
            refueling.FuelEconomy = (refueling.Odometer - old.Odometer) / refueling.FuelAmount;
        }
        else
        {
            refueling.FuelEconomy = 0;
        }

        refueling.Date = DateTime.Now;
        refueling.Active = true;

        _db.Refueling.Add(refueling);
        await _db.SaveChangesAsync();

        var x = await _db.Refueling.Select(f => f.FuelEconomy).AverageAsync();
        var v = await _db.Vehicles.Where(v => v.Id == refueling.VehicleId).SingleOrDefaultAsync();
        v!.AverageFuelEconomy = x;
        _db.Vehicles.Update(v);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteRefuelingAsync(Refueling refueling)
    {
        var x = await _db.Refueling.SingleOrDefaultAsync(v => v.Id == refueling.Id);
        if (x is not null)
        {
            x.Active = false;
            _db.Refueling.Update(x);
            await _db.SaveChangesAsync();
        }
    }

    public async Task UpdateRefuelingAsync(Refueling refueling)
    {
        _db.Refueling.Update(refueling);
        await _db.SaveChangesAsync();
    }
}
