using Aliance.Domain.Entities;
using Aliance.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface IPatrimonyRepository
{
    Task<PagedResult<Patrimony>> GetAllPatrimonies(int churchId, int pageNumber, int pageSize);

    Task<Patrimony> GetPatrimonyByGuid(int churchId, Guid guid);

    Task<Patrimony> InsertPatrimony(Patrimony patrimony);

    Task<Patrimony> UpdatePatrimony(int churchId, Patrimony patrimony);

    Task<Patrimony> DeletePatrimony(int churchId, int id);

    Task<Patrimony> GetByGuidWithDocumentsAsync(int churchId, Guid guid);

    Task<int> PatrimoniesCount(int churchId);
}
