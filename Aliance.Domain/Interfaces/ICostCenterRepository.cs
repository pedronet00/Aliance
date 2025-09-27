using Aliance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface ICostCenterRepository
{
    Task<IEnumerable<CostCenter>> GetAllCenters(int churchId);

    Task<CostCenter> GetById(int churchId, int id);

    Task<CostCenter> Add(CostCenter costCenter);

    Task<CostCenter> Update(int churchId,CostCenter costCenter);

    Task<bool> Delete(int churchId,int id);

    Task<CostCenter> Deactivate(int churchId, int id);

    Task<CostCenter> Activate(int churchId, int id);

}
