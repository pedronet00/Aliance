using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Pagination;
using Aliance.Infrastructure.Context;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.Repositories;

public class DepartmentMemberRepository : IDepartmentMemberRepository
{
    private readonly AppDbContext _context;

    public DepartmentMemberRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DepartmentMember> DeleteDepartmentMember(int departmentId, string memberId)
    {
        var member = await _context.DepartmentMember
            .Where(dm => dm.DepartmentId == departmentId && dm.UserId == memberId)
            .FirstOrDefaultAsync();

        _context.DepartmentMember.Remove(member);

        return member;
    }

    public async Task<PagedResult<DepartmentMember>> GetDepartmentMembers(int churchId, int departmentId, int pageNumber, int pageSize)
    {
        var query = _context.DepartmentMember
            .Where(dm => dm.DepartmentId == departmentId && dm.Department.ChurchId == churchId)
            .Include(dm => dm.User)
            .AsNoTracking();

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<DepartmentMember>(
            items,
            totalCount,
            pageNumber,
            pageSize
        );
    }

    public async Task<DepartmentMember> GetMemberById(int churchId, int departmentId, string memberId)
    {
        var member = await _context.DepartmentMember
            .Include(dm => dm.User)
            .Where(dm => dm.DepartmentId == departmentId && dm.UserId == memberId)
            .FirstOrDefaultAsync();

        return member;
    }

    public async Task<DepartmentMember> InsertDepartmentMember(int departmentId, string memberId)
    {
        var member = new DepartmentMember(departmentId, memberId);

        await _context.DepartmentMember.AddAsync(member);

        return member;
    }
}
