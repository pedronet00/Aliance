using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface IUnitOfWork
{

    Task<bool> Commit();

    public Task Rollback();
}
