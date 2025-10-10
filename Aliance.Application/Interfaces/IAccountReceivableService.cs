using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface IAccountReceivableService
{
    Task<IEnumerable<AccountReceivableViewModel>> GetAllAsync();

    Task<AccountReceivableViewModel?> GetByGuidAsync(Guid guid);

    Task<AccountReceivableViewModel> AddAsync(AccountReceivableDTO accountReceivable);

    Task<AccountReceivableViewModel> UpdateAsync(AccountReceivableDTO accountReceivable);

    Task<bool> DeleteAsync(Guid guid);

    Task<AccountReceivableViewModel> ToggleStatus(Guid guid, AccountStatus status);
}
