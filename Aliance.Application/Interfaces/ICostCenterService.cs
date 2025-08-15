using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface ICostCenterService 
{
    Task<IEnumerable<CostCenterViewModel>> GetAllCenters();

    Task<CostCenterViewModel> GetById(int id);

    Task<CostCenterViewModel> Add(CostCenterDTO costCenterDTO);

    Task<CostCenterViewModel> Update(CostCenterDTO costCenterDTO);

    Task<bool> Delete(int id);
}
