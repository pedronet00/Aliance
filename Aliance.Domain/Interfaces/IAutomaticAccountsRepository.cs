using Aliance.Domain.Entities;
using Aliance.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface IAutomaticAccountsRepository
{
    Task<PagedResult<AutomaticAccounts>> GetAllPaged(int churchId, int pageNumber, int pageSize);

    Task<IEnumerable<AutomaticAccounts>> GetAll();

    Task<AutomaticAccounts> Insert(AutomaticAccounts automaticAccount);

    Task<AutomaticAccounts> Update(AutomaticAccounts automaticAccount);

    Task<AutomaticAccounts> Delete(int id);

    Task<AutomaticAccounts> GetById(int id);

    Task<AutomaticAccounts> GetByGuid(Guid guid);
}
