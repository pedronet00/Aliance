using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
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

public class PastoralVisitRepository : IPastoralVisitRepository
{
    private readonly AppDbContext _context;

    public PastoralVisitRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PastoralVisit> DeletePastoralVisit(int churchId, Guid guid)
    {
        var visit = await _context.PastoralVisit
            .FirstOrDefaultAsync(v => v.Guid == guid && v.ChurchId == churchId);

        _context.PastoralVisit.Remove(visit);

        return visit;
    }

    public async Task<PagedResult<PastoralVisit>> GetAllVisits(int churchId, int pageNumber, int pageSize)
    {
        var query = _context.PastoralVisit
            .Where(v => v.ChurchId == churchId)
            .Include(v => v.VisitedMember)
            .Include(v => v.Pastor);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<PastoralVisit>(items, totalCount, pageNumber, pageSize);
    }

    public async Task<PastoralVisit> GetPastoralVisitByGuid(int churchId, Guid guid)
    {
        var visit = await _context.PastoralVisit
            .Include(v => v.VisitedMember)
            .Include(v => v.Pastor)
            .FirstOrDefaultAsync(v => v.Guid == guid && v.ChurchId == churchId);

        return visit;
    }

    public async Task<PastoralVisit> AddPastoralVisit(PastoralVisit visit)
    {
        await _context.AddAsync(visit);

        return visit;
    }

    public async Task<PastoralVisit> TogglePastoralVisitStatus(int churchId, Guid guid, MeetingStatus status)
    {
        var visit = await _context.PastoralVisit
            .FirstOrDefaultAsync(v => v.Guid == guid && v.ChurchId == churchId);

        switch (status)
        {
            case MeetingStatus.Completado:
                visit!.Status = MeetingStatus.Completado;
                break;

            case MeetingStatus.Cancelado:
                visit!.Status = MeetingStatus.Cancelado;
                break;

            case MeetingStatus.Adiado:
                visit!.Status = MeetingStatus.Adiado;
                break;
        }

        _context.PastoralVisit.Update(visit);

        return visit;
    }

    public async Task<PastoralVisit> UpdatePastoralVisit(int churchId, PastoralVisit visit)
    {
        _context.PastoralVisit.Update(visit);

        return visit;
    }
}
