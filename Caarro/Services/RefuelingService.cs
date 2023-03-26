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

    public async Task<List<Refueling?>> GetAllRefuelingsAsync(int carId)
    {
        return (await _db.Refueling.Where(r => r.VehicleId == carId && r.Active == true).ToListAsync())!;
    }

    public async Task<Refueling?> GetRefuelingAsync(int id)
    {
        return await _db.Refueling.SingleOrDefaultAsync(v => v.Id == id);
    }

    public async Task AddRefuelingAsync(Refueling refueling)
    {
        refueling.Date = DateTime.Now;
        refueling.Active = true;

        _db.Refueling.Add(refueling);
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
