using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Pagination;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly AppDbContext _context;

    public LocationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Location> GetLocationByGuid(int churchId, Guid guid)
    {
        var location = await _context.Location
            .FirstOrDefaultAsync(l => l.Guid == guid && l.ChurchId == churchId);

        return location;
    }

    public async Task<PagedResult<Location>> GetLocations(int churchId, int pageNumber, int pageSize)
    {
        var query = _context.Location
            .Where(l => l.ChurchId == churchId)
            .OrderBy(l => l.Name)
            .AsNoTracking(); 

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Location>(items, totalCount, pageNumber, pageSize);

    }

    public async Task<Location> InsertLocation(Location location)
    {
        await _context.Location.AddAsync(location);

        return location;
    }
}
