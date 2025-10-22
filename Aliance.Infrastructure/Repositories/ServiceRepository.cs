using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Pagination;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Aliance.Infrastructure.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly AppDbContext _context;

    public ServiceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Service> AddService(Service service)
    {
        await _context.Service.AddAsync(service);

        return service;
    }

    public async Task<bool> DeleteService(Guid serviceGuid, int churchId)
    {
        var service = await _context.Service
            .FirstOrDefaultAsync(s => s.Guid == serviceGuid && s.ChurchId == churchId);

        _context.Service.Remove(service!);

        return true;
    }

    public async Task<Service> GetServiceByGuid(Guid serviceGuid, int churchId)
    {
        var service = await _context.Service
            .Include(s => s.Location)
            .FirstOrDefaultAsync(s => s.Guid == serviceGuid && s.ChurchId == churchId);

        return service!;
    }

    public async Task<Service> GetServiceById(int churchId, int serviceId)
    {
        var service = await _context.Service
            .Where(s => s.ChurchId == churchId && s.Id == serviceId)
            .FirstOrDefaultAsync();

        return service!;
    }

    public async Task<PagedResult<Service>> GetServices(int churchId, int pageNumber, int pageSize)
    {
        var query = _context.Service
            .Include(s => s.Location)
            .Where(s => s.ChurchId == churchId);

        var totalCount = await query.CountAsync();

        var services = await query
            .OrderByDescending(s => s.Date)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var pagedServices = new PagedResult<Service>(services, totalCount, pageNumber, pageSize);
        return pagedServices;
    }

    public async Task<bool> ServiceExists(int locationId, DateTime date, int churchId)
    {
        var exists = await _context.Service
            .AnyAsync(s => s.LocationId == locationId && s.Date == date && s.ChurchId == churchId);

        return exists;
    }

    public async Task<Service> ToggleStatus(Guid serviceGuid, int churchId, ServiceStatus status)
    {
        var service = await _context.Service
            .FirstOrDefaultAsync(s => s.Guid == serviceGuid && s.ChurchId == churchId);

        service!.Status = status;
        _context.Service.Update(service);

        return service;
    }

    public async Task<Service> UpdateService(Service service)
    {
        _context.Service.Update(service);

        return service;
    }
}
