using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Aliance.Infrastructure.Repositories;

public class CellMemberRepository : ICellMemberRepository
{
    private readonly AppDbContext _context;

    public CellMemberRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CellMember>> GetCellMembers(int churchId, Guid cellGuid)
    {
        return await _context.CellMember
            .Include(cm => cm.User)
            .Where(cm => cm.CellGuid == cellGuid)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<CellMember> InsertCellMember(Guid cellGuid, string memberId)
    {
        var member = new CellMember(cellGuid, memberId, true);

        await _context.CellMember.AddAsync(member);
        return member;
    }

    public async Task<CellMember> DeleteCellMember(Guid cellGuid, string memberId)
    {
        var member = await _context.CellMember
            .FirstOrDefaultAsync(cm => cm.CellGuid == cellGuid && cm.UserId == memberId);

        if (member != null)
            _context.CellMember.Remove(member);

        return member;
    }

    public async Task<CellMember> ToggleMemberStatus(int churchId, Guid cellGuid, string memberId, bool status)
    {
        var member = await _context.CellMember
            .FirstOrDefaultAsync(cm => cm.CellGuid == cellGuid && cm.UserId == memberId);

        if (member != null)
        {
            member.Status = status;
            _context.CellMember.Update(member);
        }

        return member;
    }
}
