using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Aliance.Infrastructure.Repositories;

public class ServiceRoleRepository : IServiceRoleRepository
{
    private readonly AppDbContext _context;

    public ServiceRoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceRole> Add(ServiceRole serviceRole)
    {
        await _context.ServiceRole.AddAsync(serviceRole);

        return serviceRole;
    }

    public async Task<bool> Delete(Guid serviceRoleGuid, int churchId)
    {
        var serviceRole = await _context.ServiceRole
            .FirstOrDefaultAsync(sr => sr.Guid == serviceRoleGuid && sr.Service.ChurchId == churchId);

        _context.ServiceRole.Remove(serviceRole!);

        return true;
    }

    public async Task<IEnumerable<ServiceRole>> List(int serviceId)
    {
        var roles =  await _context.ServiceRole
            .Include(sr => sr.Member)
            .Where(sr => sr.ServiceId == serviceId)
            .AsNoTracking()
            .ToListAsync();

        return roles!;
    }
}
