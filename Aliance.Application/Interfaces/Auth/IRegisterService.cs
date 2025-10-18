using Aliance.Application.DTOs.Auth;
using Aliance.Application.ViewModel.Auth;
using Aliance.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces.Auth;

public interface IRegisterService
{
    Task<DomainNotificationsResult<NewClientViewModel>> Register(NewClientDTO newClientDTO);
}
