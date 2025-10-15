using Aliance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface ITitheRepository
{
    Task<IEnumerable<Tithe>> GetTithes(int churchId);

    Task<Tithe> GetTitheByGuid(int churchId, Guid guid);
    Task<Tithe> AddTithe(Tithe tithe);

    Task<Tithe> DeleteTithe(int churchId, Guid guid);

    Task<Tithe> UpdateTithe(Tithe tithe);

    Task<IEnumerable<Tithe>> GetTithesByUser(int churchId, string userId);

    Task<decimal> GetTotalTithesByUser(int churchId, string userId);

    Task<decimal> GetTotalTithes(int churchId);

}
