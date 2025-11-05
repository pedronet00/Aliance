using Aliance.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface IRoutineService
{
    Task<DomainNotificationsResult<string>> UpdateExpiringAccountsStatus();

    Task<DomainNotificationsResult<string>> InsertAutomaticAccounts();
}
