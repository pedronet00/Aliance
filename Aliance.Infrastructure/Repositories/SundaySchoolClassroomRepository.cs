using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Pagination;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.Repositories;

public class SundaySchoolClassroomRepository : ISundaySchoolClassroomRepository
{
    private readonly AppDbContext _context;

    public SundaySchoolClassroomRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<SundaySchoolClassroom> GetSundaySchoolClassroomByGuid(int churchId, Guid guid)
    {
        var classroom = await _context.SundaySchoolClassroom
            .FirstOrDefaultAsync(l => l.Guid == guid && l.ChurchId == churchId);

        return classroom;
    }

    public async Task<PagedResult<SundaySchoolClassroom>> GetSundaySchoolClassrooms(int churchId, int pageNumber, int pageSize)
    {
        var query = _context.SundaySchoolClassroom
            .Where(l => l.ChurchId == churchId)
            .OrderBy(l => l.Name)
            .AsNoTracking();

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<SundaySchoolClassroom>(items, totalCount, pageNumber, pageSize);

    }

    public async Task<SundaySchoolClassroom> InsertSundaySchoolClassroom(SundaySchoolClassroom classroom)
    {
        await _context.SundaySchoolClassroom.AddAsync(classroom);

        return classroom;
    }
}
