using Caarro.Data;

using Microsoft.EntityFrameworkCore;

namespace Caarro.Services;

public class ServicesService
{
    private readonly ILogger<ServicesService> _log;
    private readonly CaarroDbContext _db;

    public ServicesService(ILogger<ServicesService> logger, CaarroDbContext db)
    {
        _log = logger;
        _db = db;
    }

    public async Task<IEnumerable<Service>> GetAllServicesAsync(int carId, CancellationToken ct)
    {
        return await _db.Services.Where(s => s.VehicleId == carId && s.Active == true).ToListAsync(ct);
    }

    public async Task<Service?> GetServiceAsync(int id, CancellationToken ct)
    {
        return await _db.Services.SingleOrDefaultAsync(s => s.Id == id, ct);
    }

    public async Task AddServiceAsync(Service service, CancellationToken ct)
    {
        service.Date = DateTime.Now;
        service.Active = true;

        _db.Services.Add(service);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteServiceAsync(Service service, CancellationToken ct)
    {
        var x = await _db.Services.SingleOrDefaultAsync(s => s.Id == service.Id, ct);
        if (x is not null)
        {
            x.Active = false;
            _db.Services.Update(x);
            await _db.SaveChangesAsync(ct);
        }
    }

    public async Task UpdateServiceAsync(Service service, CancellationToken ct)
    {
        _db.Services.Update(service);
        await _db.SaveChangesAsync(ct);
    }
}
