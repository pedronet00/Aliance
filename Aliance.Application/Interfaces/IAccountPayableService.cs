using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface IAccountPayableService
{
    Task<PagedResult<AccountPayableViewModel>> GetAllAsync(int pageNumber, int pageSize);

    Task<AccountPayableViewModel?> GetByGuidAsync(Guid guid);

    Task<AccountPayableViewModel> AddAsync(AccountPayableDTO accountPayable);

    Task<AccountPayableViewModel> UpdateAsync(AccountPayableDTO accountPayable);

    Task<bool> DeleteAsync(Guid guid);

    Task<AccountPayableViewModel> ToggleStatus(Guid guid, AccountStatus status);
}
