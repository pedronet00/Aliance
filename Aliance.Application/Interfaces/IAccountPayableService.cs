using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface IAccountPayableService
{
    Task<IEnumerable<AccountPayableViewModel>> GetAllAsync();

    Task<AccountPayableViewModel?> GetByIdAsync(int id);

    Task<AccountPayableViewModel> AddAsync(AccountPayableDTO accountPayable);

    Task<AccountPayableViewModel> UpdateAsync(AccountPayableDTO accountPayable);

    Task<bool> DeleteAsync(int id);
}
