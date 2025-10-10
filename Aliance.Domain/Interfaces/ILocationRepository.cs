using Aliance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface ILocationRepository
{
    Task<IEnumerable<Location>> GetLocations(int churchId);

    Task<Location> GetLocationByGuid(int churchId, Guid guid);
}
