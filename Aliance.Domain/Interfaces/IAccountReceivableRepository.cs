using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface IAccountReceivableRepository
{
    Task<IEnumerable<AccountReceivable>> GetAllAsync(int churchId);

    Task<AccountReceivable?> GetByGuidAsync(int churchId, Guid guid);

    Task<AccountReceivable> AddAsync(AccountReceivable accountReceivable);

    Task<AccountReceivable> UpdateAsync(int churchId, AccountReceivable accountReceivable);

    Task<bool> DeleteAsync(int churchId, Guid guid);

    Task<AccountReceivable> ToggleStatus(int churchId, Guid guid, AccountStatus status);
}
