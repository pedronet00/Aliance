using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface IAccountPayableRepository
{
    Task<IEnumerable<AccountPayable>> GetAllAsync(int churchId);

    Task<AccountPayable?> GetByGuidAsync(int churchId, Guid guid);

    Task<AccountPayable> AddAsync(AccountPayable accountPayable);

    Task<AccountPayable> UpdateAsync(int churchId,AccountPayable accountPayable);

    Task<bool> DeleteAsync(int churchId,Guid guid);

    Task<AccountPayable> ToggleStatus(int churchId, Guid guid, AccountStatus status);
}
