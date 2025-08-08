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
    private readonly IUnitOfWork _unitOfWork;

    public DepartmentRepository(AppDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> DeleteDepartment(int id)
    {
        var department = await _context.Department.FindAsync(id);

        if(department is null)
            return false;

        _context.Remove(department);
        await _unitOfWork.Commit();

        return true;
    }

    public async Task<IEnumerable<Department>> GetAllDepartments()
    {
        return await _context.Department.AsNoTracking().ToListAsync();
    }

    public async Task<Department> GetDepartmentById(int id)
    {
        var department =  await _context.Department
            .FirstOrDefaultAsync(x => x.Id == id);

        if (department is null)
            return null;

        return department;
    }

    public async Task<Department> InsertDepartment(Department department)
    {

        if (department is null)
            throw new ArgumentNullException(nameof(department));

        _context.Department.Add(department);
        await _unitOfWork.Commit();

        return department;
    }

    public async Task<bool> UpdateDepartment(Department department)
    {
   
        if (department is null)
            return false;

        _context.Entry(department).State = EntityState.Modified;
        await _unitOfWork.Commit();

        return true;
    }
}
