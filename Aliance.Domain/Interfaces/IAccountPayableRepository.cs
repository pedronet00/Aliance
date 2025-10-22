using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface IAccountPayableRepository
{
    Task<PagedResult<AccountPayable>> GetAllAsync(int churchId, int pageSize, int pageNumber);

    Task<AccountPayable?> GetByGuidAsync(int churchId, Guid guid);

    Task<AccountPayable> AddAsync(AccountPayable accountPayable);

    Task<AccountPayable> UpdateAsync(int churchId,AccountPayable accountPayable);

    Task<bool> DeleteAsync(int churchId,Guid guid);

    Task<AccountPayable> ToggleStatus(int churchId, Guid guid, AccountStatus status);
}
