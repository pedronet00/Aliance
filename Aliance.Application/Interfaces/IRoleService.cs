using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface IRoleService
{
    Task<List<string>> GetAllRolesAsync();
    Task<bool> CreateRoleAsync(string roleName);
}
