using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface ICostCenterService 
{
    Task<IEnumerable<CostCenterViewModel>> GetAllCenters();

    Task<DomainNotificationsResult<CostCenterViewModel>> GetById(int id);

    Task<DomainNotificationsResult<CostCenterViewModel>> Add(CostCenterDTO costCenterDTO);

    Task<DomainNotificationsResult<CostCenterViewModel>> Update(CostCenterDTO costCenterDTO);

    Task<DomainNotificationsResult<bool>> Delete(int id);

    Task<DomainNotificationsResult<CostCenterViewModel>> Deactivate(int id);

    Task<DomainNotificationsResult<CostCenterViewModel>> Activate(int id);

}
