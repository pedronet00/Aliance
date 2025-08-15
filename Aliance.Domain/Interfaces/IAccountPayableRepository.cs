using Aliance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface IAccountPayableRepository
{
    Task<IEnumerable<AccountPayable>> GetAllAsync();

    Task<AccountPayable?> GetByIdAsync(int id);

    Task<AccountPayable> AddAsync(AccountPayable accountPayable);

    Task<AccountPayable> UpdateAsync(AccountPayable accountPayable);

    Task<bool> DeleteAsync(int id);
}
