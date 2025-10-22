using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Enums;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface IServiceService
{
    Task<DomainNotificationsResult<PagedResult<ServiceViewModel>>> GetServices(int pageNumber, int pageSize);

    Task<DomainNotificationsResult<ServiceViewModel>> GetServiceByGuid(Guid serviceGuid);

    Task<DomainNotificationsResult<ServiceViewModel>> AddService(ServiceDTO serviceDTO);

    Task<DomainNotificationsResult<ServiceViewModel>> ToggleStatus(Guid serviceGuid, ServiceStatus status);

    Task<DomainNotificationsResult<bool>> DeleteService(Guid serviceGuid);

    Task<DomainNotificationsResult<ServiceViewModel>> UpdateService(ServiceDTO serviceDTO);
}
