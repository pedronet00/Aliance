using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Pagination;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Aliance.Infrastructure.Repositories;

public class EventRepository : IEventRepository
{
    private readonly AppDbContext _context;

    public EventRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Event> AddEvent(Event newEvent)
    {
        var ev = await _context.Event.AddAsync(newEvent);

        return newEvent;
    }

    public async Task<int> CountEvents(int churchId)
    {
        var totalEvents = await _context.Event
            .Where(ev => ev.ChurchId == churchId)
            .CountAsync();

        return totalEvents;
    }

    public async Task<bool> DeleteEvent(int churchId, Guid guid)
    {
        var ev = await _context.Event.Where(e => e.ChurchId == churchId && e.Guid == guid).FirstOrDefaultAsync();

        _context.Event.Remove(ev);

        return true;
    }

    public async Task<Event> GetEventByGuid(int churchId, Guid guid)
    {
        var ev = await _context.Event
            .Include(e => e.Location)
            .Where(e => e.ChurchId == churchId && e.Guid == guid)
            .FirstOrDefaultAsync();

        return ev;
    }

    public async Task<PagedResult<Event>> GetEvents(int churchId, int pageNumber, int pageSize)
    {
        var query = _context.Event
            .Where(e => e.ChurchId == churchId)
            .OrderBy(e => e.Date)
            .Include(e => e.Location);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Event>(items, totalCount, pageNumber, pageSize);
    }

    public async Task<IEnumerable<Event>> GetEventsByDateRange(int churchId, DateTime startDate, DateTime endDate)
    {
        var events = await _context.Event
            .Where(e => e.ChurchId == churchId && e.Date >= startDate && e.Date <= endDate)
            .Include(e => e.Location)
            .ToListAsync();

        return events;
    }

    public async Task<Event> GetNextEvent(int churchId)
    {
        var nextEvent = await _context.Event
            .Where(e => e.ChurchId == churchId && e.Date >= DateTime.Now)
            .OrderBy(e => e.Date)
            .Include(e => e.Location)
            .FirstOrDefaultAsync();

        return nextEvent;
    }

    public async Task<Event> ToggleStatus(int churchId, Guid guid, MeetingStatus status)
    {
        var ev = await _context.Event
            .FirstOrDefaultAsync(e => e.ChurchId == churchId && e.Guid == guid);

        if (ev == null)
            throw new InvalidOperationException("Evento não encontrado.");

        switch (status)
        {
            case MeetingStatus.Adiado:
                ev.Status = MeetingStatus.Adiado;
                break;
            case MeetingStatus.Completado:
                ev.Status = MeetingStatus.Completado;
                break;
            case MeetingStatus.Cancelado:
                ev.Status = MeetingStatus.Cancelado;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(status), "Invalid meeting status.");
        }

        _context.Update(ev);
        await _context.SaveChangesAsync();

        return ev;
    }


    public async Task<Event> UpdateEvent(Event eventUpdated)
    {
        _context.Event.Update(eventUpdated);

        return eventUpdated;
    }
}
