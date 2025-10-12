using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
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

    public async Task<IEnumerable<Event>> GetEvents(int churchId)
    {
        var events = await _context.Event
            .Where(e => e.ChurchId == churchId)
            .Include(e => e.Location)
            .ToListAsync();

        return events;
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

    public async Task<Event> UpdateEvent(Event eventUpdated)
    {
        _context.Event.Update(eventUpdated);

        return eventUpdated;
    }
}
