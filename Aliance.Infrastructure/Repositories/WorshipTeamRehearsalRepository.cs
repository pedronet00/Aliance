using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Aliance.Infrastructure.Repositories;

public class WorshipTeamRehearsalRepository : IWorshipTeamRehearsalRepository
{
    private readonly AppDbContext _context;

    public WorshipTeamRehearsalRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<WorshipTeamRehearsal> DeleteRehearsal(int churchId, Guid guid)
    {
        var rehearsal = await _context.WorshipTeamRehearsal
            .Include(r => r.WorshipTeam)
            .FirstOrDefaultAsync(r => r.Guid == guid && r.WorshipTeam.ChurchId == churchId);

        _context.WorshipTeamRehearsal.Remove(rehearsal);

        return rehearsal;
    }

    public async Task<WorshipTeamRehearsal> GetNextRehearsal(int churchId, int teamId)
    {
        var nextRehearsal = await _context.WorshipTeamRehearsal
            .AsNoTracking()
            .Where(r => r.Status == Domain.Enums.MeetingStatus.Agendado
                        && r.RehearsalDate >= DateTime.Now
                        && r.WorshipTeamId == teamId)
            .OrderBy(r => r.RehearsalDate) 
            .FirstOrDefaultAsync();

        return nextRehearsal;
    }

    public async Task<WorshipTeamRehearsal> GetRehearsalByGuid(int churchId, Guid guid)
    {
        var rehearsal = await _context.WorshipTeamRehearsal
            .Include(r => r.WorshipTeam)
            .FirstOrDefaultAsync(r => r.Guid == guid && r.WorshipTeam.ChurchId == churchId);

        return rehearsal;
    }

    public async Task<IEnumerable<WorshipTeamRehearsal>> GetTeamRehearsals(int churchId, int teamId)
    {
        var teamRehearsals = await _context.WorshipTeamRehearsal
            .Where(r => r.WorshipTeamId == teamId && r.WorshipTeam.ChurchId == churchId)
            .ToListAsync();

        return teamRehearsals;
    }

    public async Task<WorshipTeamRehearsal> InsertRehearsal(WorshipTeamRehearsal rehearsal)
    {
        await _context.WorshipTeamRehearsal.AddAsync(rehearsal);

        return rehearsal;
    }

    public async Task<WorshipTeamRehearsal> UpdateRehearsal(int churchId, WorshipTeamRehearsal rehearsal)
    {
        _context.WorshipTeamRehearsal.Update(rehearsal);

        return rehearsal;
    }
}
