using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Aliance.Domain.Pagination;

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

    public async Task<PagedResult<Department>> GetAllDepartments(int churchId, int pageNumber, int pageSize)
    {
        var query = _context.Department
            .Where(d => d.ChurchId == churchId)
            .OrderBy(d => d.Name)
            .AsNoTracking();

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Department>(items, totalCount, pageNumber, pageSize);
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
