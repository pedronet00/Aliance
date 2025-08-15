using Aliance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface ICostCenterRepository
{
    Task<IEnumerable<CostCenter>> GetAllCenters();

    Task<CostCenter> GetById(int id);

    Task<CostCenter> Add(CostCenter costCenter);

    Task<CostCenter> Update(CostCenter costCenter);

    Task<bool> Delete(int id);
}
