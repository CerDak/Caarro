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

    public async Task<List<Service?>> GetAllServicesAsync(int carId)
    {
        return (await _db.Services.Where(s => s.VehicleId == carId && s.Active == true).ToListAsync())!;
    }

    public async Task<Service?> GetServiceAsync(int id)
    {
        return await _db.Services.SingleOrDefaultAsync(s => s.Id == id);
    }

    public async Task AddServiceAsync(Service service)
    {
        service.Date = DateTime.Now;
        service.Active = true;

        _db.Services.Add(service);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteServiceAsync(Service service)
    {
        var x = await _db.Services.SingleOrDefaultAsync(s => s.Id == service.Id);
        if (x is not null)
        {
            x.Active = false;
            _db.Services.Update(x);
            await _db.SaveChangesAsync();
        }
    }

    public async Task UpdateServiceAsync(Service service)
    {
        _db.Services.Update(service);
        await _db.SaveChangesAsync();
    }
}
