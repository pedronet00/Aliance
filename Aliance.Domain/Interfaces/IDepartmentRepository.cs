﻿using Aliance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface IDepartmentRepository
{
    Task<IEnumerable<Department>> GetAllDepartments(int churchId);

    Task<Department> GetDepartmentById(int churchId, int id);

    Task<Department> InsertDepartment(Department department);

    Task<bool> UpdateDepartment(int churchId,Department department);

    Task<bool> DeleteDepartment(int churchId,int id);

    Task<Department> ActivateDepartment(int churchId, int id);

    Task<Department> DeactivateDepartment(int churchId, int id);
}
