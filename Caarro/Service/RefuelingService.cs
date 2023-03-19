using Caarro.Data;
using Microsoft.EntityFrameworkCore;

namespace Caarro.Service;

public class RefuelingService
{
    private ILogger<RefuelingService> _log;
    private readonly CaarroDbContext _db;

    public RefuelingService(ILogger<RefuelingService> logger, CaarroDbContext db)
    {
        _log = logger;
        _db = db;
    }

    public async Task<List<Refueling?>> GetAllRefuelings(int carId)
    {
        return (await _db.Refueling.Where(r => r.VehicleId == carId && r.Active == true).ToListAsync())!;
    }

    public async Task<Refueling?> GetRefueling(int id)
    {
        return await _db.Refueling.SingleOrDefaultAsync(v => v.Id == id);
    }

    public async Task AddRefueling(Refueling refueling)
    {
        refueling.Date = DateTime.Now;

        _db.Refueling.Add(refueling);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteRefueling(Refueling refueling)
    {
        var x = await _db.Refueling.SingleOrDefaultAsync(v => v.Id == refueling.Id);
        if (x is not null)
        {
            x.Active = false;
            _db.Refueling.Update(x);
            await _db.SaveChangesAsync();
        }
    }

    public async Task UpdateRefueling(Refueling refueling)
    {
        _db.Refueling.Update(refueling);
        await _db.SaveChangesAsync();
    }
}