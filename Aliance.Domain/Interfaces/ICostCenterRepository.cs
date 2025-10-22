using Aliance.Domain.Entities;
using Aliance.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface ICostCenterRepository
{
    Task<PagedResult<CostCenter>> GetAllCenters(int churchId, int pageNumber, int pageSize);

    Task<CostCenter> GetByGuid(int churchId, Guid guid);

    Task<CostCenter> Add(CostCenter costCenter);

    Task<CostCenter> Update(int churchId,CostCenter costCenter);

    Task<bool> Delete(int churchId, Guid guid);

    Task<CostCenter> Deactivate(int churchId, Guid guid);

    Task<CostCenter> Activate(int churchId, Guid guid);

}
