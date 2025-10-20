using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Enums;
using Aliance.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface IServiceService
{
    Task<DomainNotificationsResult<IEnumerable<ServiceViewModel>>> GetServices();

    Task<DomainNotificationsResult<ServiceViewModel>> GetServiceByGuid(Guid serviceGuid);

    Task<DomainNotificationsResult<ServiceViewModel>> AddService(ServiceDTO serviceDTO);

    Task<DomainNotificationsResult<ServiceViewModel>> ToggleStatus(Guid serviceGuid, ServiceStatus status);

    Task<DomainNotificationsResult<bool>> DeleteService(Guid serviceGuid);

    Task<DomainNotificationsResult<ServiceViewModel>> UpdateService(ServiceDTO serviceDTO);
}
