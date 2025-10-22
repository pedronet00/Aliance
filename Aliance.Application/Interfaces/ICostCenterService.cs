using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface ICostCenterService 
{
    Task<PagedResult<CostCenterViewModel>> GetAllCenters(int pageNumber, int pageSize);

    Task<DomainNotificationsResult<CostCenterViewModel>> GetByGuid(Guid guid);

    Task<DomainNotificationsResult<CostCenterViewModel>> Add(CostCenterDTO costCenterDTO);

    Task<DomainNotificationsResult<CostCenterViewModel>> Update(CostCenterDTO costCenterDTO);

    Task<DomainNotificationsResult<bool>> Delete(Guid guid);

    Task<DomainNotificationsResult<CostCenterViewModel>> Deactivate(Guid guid);

    Task<DomainNotificationsResult<CostCenterViewModel>> Activate(Guid guid);

}
