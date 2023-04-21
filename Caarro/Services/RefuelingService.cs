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

    public async Task<IEnumerable<Refueling>> GetAllRefuelingsAsync(int carId, CancellationToken ct)
    {
        return await _db.Refueling.Where(r => r.VehicleId == carId && r.Active == true).ToListAsync(ct);
    }

    public async Task<Refueling?> GetRefuelingAsync(int id, CancellationToken ct)
    {
        return await _db.Refueling.SingleOrDefaultAsync(v => v.Id == id, ct);
    }

    public async Task AddRefuelingAsync(Refueling refueling, CancellationToken ct)
    {
        var old = await _db.Refueling
            .FromSqlInterpolated(
                $"SELECT * FROM Refueling WHERE VehicleId = {refueling.VehicleId} ORDER BY ID DESC LIMIT 1")
            .SingleOrDefaultAsync(ct);
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
        await _db.SaveChangesAsync(ct);

        var x = await _db.Refueling.Select(f => f.FuelEconomy).AverageAsync(ct);
        var v = await _db.Vehicles.Where(v => v.Id == refueling.VehicleId).SingleOrDefaultAsync(ct);
        v!.AverageFuelEconomy = x;
        _db.Vehicles.Update(v);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteRefuelingAsync(Refueling refueling, CancellationToken ct)
    {
        var x = await _db.Refueling.SingleOrDefaultAsync(v => v.Id == refueling.Id, ct);
        if (x is not null)
        {
            x.Active = false;
            _db.Refueling.Update(x);
            await _db.SaveChangesAsync(ct);
        }
    }

    public async Task UpdateRefuelingAsync(Refueling refueling, CancellationToken ct)
    {
        _db.Refueling.Update(refueling);
        await _db.SaveChangesAsync(ct);
    }
}
