using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.Repositories;

public class DepartmentRepository : IDepartmentRepository
{

    private readonly AppDbContext _context;

    public DepartmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Department> ActivateDepartment(int churchId, int id)
    {
        var department = await _context.Department.Where(d => d.Id == id && d.ChurchId == churchId).FirstOrDefaultAsync();

        department.Status = true;
        _context.Update(department);

        return department;
    }

    public async Task<Department> DeactivateDepartment(int churchId, int id)
    {
        var department = await _context.Department.Where(d => d.Id == id && d.ChurchId == churchId).FirstOrDefaultAsync();

        department.Status = false;
        _context.Update(department);

        return department;
    }

    public async Task<bool> DeleteDepartment(int churchId, int id)
    {
        var department = await _context.Department.FindAsync(id);

        _context.Remove(department);

        return true;
    }

    public async Task<IEnumerable<Department>> GetAllDepartments(int churchId)
    {
        return await _context.Department.AsNoTracking().ToListAsync();
    }

    public async Task<Department> GetDepartmentById(int churchId, int id)
    {
        var department =  await _context.Department
            .FirstOrDefaultAsync(x => x.Id == id);

        return department;
    }

    public async Task<Department> InsertDepartment(Department department)
    {
        _context.Department.Add(department);

        return department;
    }

    public async Task<bool> UpdateDepartment(int churchId, Department department)
    {
        _context.Department.Update(department);

        return true;
    }
}
