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

public class WorshipTeamMemberRepository : IWorshipTeamMemberRepository
{
    private readonly AppDbContext _context;

    public WorshipTeamMemberRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<WorshipTeamMember> DeleteWorshipTeamMember(int teamId, string memberId)
    {
        var member = await _context.WorshipTeamMember
            .Where(t => t.WorshipTeamId == teamId && t.UserId == memberId)
            .FirstOrDefaultAsync();

        _context.WorshipTeamMember.Remove(member);

        return member;
    }

    public async Task<PagedResult<WorshipTeamMember>> GetWorshipTeamMembers(int churchId, int teamId, int pageNumber, int pageSize)
    {
        var query = _context.WorshipTeamMember
            .Include(wtm => wtm.WorshipTeam)
            .Include(wtm => wtm.User)
            .Where(wtm => wtm.WorshipTeam.ChurchId == churchId && wtm.WorshipTeamId == teamId);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<WorshipTeamMember>(items, totalCount, pageNumber, pageSize);
    }

    public async Task<WorshipTeamMember> InsertWorshipTeamMember(int teamId, string memberId)
    {
        var newMember = new WorshipTeamMember(memberId, teamId, true);
        await _context.WorshipTeamMember.AddAsync(newMember);
        return newMember;
    }

    public async Task<WorshipTeamMember> ToggleMemberStatus(int churchId, int teamId, string memberId, bool status)
    {
        var member = await _context.WorshipTeamMember
            .Include(wtm => wtm.WorshipTeam)
            .Where(wtm => wtm.WorshipTeam.ChurchId == churchId && wtm.WorshipTeamId == teamId && wtm.UserId == memberId)
            .FirstOrDefaultAsync();
        if (member != null)
        {
            member.Status = status;
            _context.WorshipTeamMember.Update(member);
        }
        return member;
    }
}
