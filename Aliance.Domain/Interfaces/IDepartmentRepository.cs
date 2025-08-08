using Aliance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface IDepartmentRepository
{
    Task<IEnumerable<Department>> GetAllDepartments();

    Task<Department> GetDepartmentById(int id);

    Task<Department> InsertDepartment(Department department);

    Task<bool> UpdateDepartment(Department department);

    Task<bool> DeleteDepartment(int id);
}
