using Aliance.Domain.Entities;
using Aliance.Domain.Pagination;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface ILocationRepository
{
    Task<PagedResult<Location>> GetLocations(int churchId, int pageNumber, int pageSize);

    Task<Location> GetLocationByGuid(int churchId, Guid guid);

    Task<Location> UpdateLocation(Location location);

    Task<Location> InsertLocation(Location location);
}
