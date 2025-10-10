using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
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

    public async Task<IEnumerable<Location>> GetLocations(int churchId)
    {
        var locations = await _context.Location
            .Where(l => l.ChurchId == churchId)
            .ToListAsync();

        return locations;
    }
}
