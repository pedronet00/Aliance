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

public interface IServiceRoleService
{
    Task<DomainNotificationsResult<IEnumerable<ServiceRoleViewModel>>> List(Guid serviceGuid);

    Task<DomainNotificationsResult<ServiceRoleViewModel>> Add(ServiceRoleDTO serviceRoleDTO);

    Task<DomainNotificationsResult<bool>> Delete(Guid serviceRoleGuid);
}
