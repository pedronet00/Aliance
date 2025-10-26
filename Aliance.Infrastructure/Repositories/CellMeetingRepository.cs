using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Interfaces;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Aliance.Infrastructure.Repositories;

public class CellMeetingRepository : ICellMeetingRepository
{
    private readonly AppDbContext _context;

    public CellMeetingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CellMeeting> DeleteCellMeeting(int churchId, Guid guid)
    {
        var meeting = await _context.CellMeeting.FirstOrDefaultAsync(m => m.Guid == guid && m.Cell.ChurchId == churchId);

        _context.CellMeeting.Remove(meeting);

        return meeting;
    }

    public async Task<CellMeeting> GetCellMeetingByGuid(int churchId, Guid guid)
    {
        var meeting = await _context.CellMeeting
            .Include(m => m.Leader)
            .Include(m => m.Location)
            .FirstOrDefaultAsync(m => m.Guid == guid && m.Cell.ChurchId == churchId);

        return meeting;
    }

    public async Task<IEnumerable<CellMeeting>> GetCellMeetings(int churchId, Guid cellGuid)
    {
        var meetings = await _context.CellMeeting
            .Include(m => m.Leader)
            .Include(m => m.Location)
            .Where(m => m.Cell.Guid == cellGuid && m.Cell.ChurchId == churchId)
            .ToListAsync();
        
        return meetings;
    }

    public async Task<CellMeeting> GetNextCellMeeting(int churchId)
    {
        var nextMeeting = await _context.CellMeeting
            .Where(m => m.Cell.ChurchId == churchId && m.Date > DateTime.UtcNow && m.Status == MeetingStatus.Agendado)
            .OrderBy(m => m.Date)
            .FirstOrDefaultAsync();

        return nextMeeting;
    }

    public async Task<CellMeeting> InsertCellMeeting(CellMeeting cellMeeting)
    {
        await _context.CellMeeting.AddAsync(cellMeeting);

        return cellMeeting;
    }

    public async Task<CellMeeting> UpdateCellMeeting(int churchId, CellMeeting cellMeeting)
    {
        _context.CellMeeting.Update(cellMeeting); 
        
        return cellMeeting;
    }
}
