using Aliance.Domain.Entities;

namespace Aliance.Domain.Interfaces;

public interface IEventRepository
{
    Task<IEnumerable<Event>> GetEvents(int churchId);
    
    Task<Event> GetEventByGuid(int churchId, Guid guid);

    Task<Event> AddEvent(Event newEvent);

    Task<Event> UpdateEvent(Event eventUpdated);

    Task<bool> DeleteEvent(int churchId, Guid guid);

    Task<IEnumerable<Event>> GetEventsByDateRange(int churchId, DateTime startDate, DateTime endDate);

    Task<Event> GetNextEvent(int churchId);
}
