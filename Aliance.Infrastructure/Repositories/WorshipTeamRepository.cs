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

public class WorshipTeamRepository : IWorshipTeamRepository
{
    private readonly AppDbContext _context;

    public WorshipTeamRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<WorshipTeam> ActivateWorshipTeam(int churchId, int id)
    {
        var team = await _context.WorshipTeam.FirstOrDefaultAsync(t => t.Id == id && t.ChurchId == churchId);

        team.Status = true;

        _context.WorshipTeam.Update(team);

        return team;
    }

    public async Task<WorshipTeam> DeactivateWorshipTeam(int churchId, int id)
    {
        var team = await _context.WorshipTeam.FirstOrDefaultAsync(t => t.Id == id && t.ChurchId == churchId);

        team.Status = false;

        _context.WorshipTeam.Update(team);

        return team;
    }

    public async Task<bool> DeleteWorshipTeam(int churchId, int id)
    {
        var team = await _context.WorshipTeam.FirstOrDefaultAsync(t => t.Id == id && t.ChurchId == churchId);

        _context.WorshipTeam.Remove(team);

        return true;
    }

    public async Task<PagedResult<WorshipTeam>> GetAllWorshipTeams(int churchId, int pageNumber, int pageSize)
    {
        var query = _context.WorshipTeam.Where(t => t.ChurchId == churchId);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<WorshipTeam>(items, totalCount, pageNumber, pageSize);
    }

    public async Task<WorshipTeam> GetWorshipTeamByGuid(int churchId, Guid guid)
    {
        var team = await _context.WorshipTeam.FirstOrDefaultAsync(t => t.Guid == guid && t.ChurchId == churchId);

        return team;
    }

    public async Task<WorshipTeam> InsertWorshipTeam(WorshipTeam worshipTeam)
    {
        await _context.WorshipTeam.AddAsync(worshipTeam);

        return worshipTeam;
    }

    public async Task<WorshipTeam> UpdateWorshipTeam(int churchId, WorshipTeam worshipTeam)
    {
        _context.WorshipTeam.Update(worshipTeam);

        return worshipTeam;
    }
}
