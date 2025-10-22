using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Pagination;

namespace Aliance.Domain.Interfaces;

public interface IEventRepository
{
    Task<PagedResult<Event>> GetEvents(int churchId, int pageNumber, int pageSize);

    Task<Event> GetEventByGuid(int churchId, Guid guid);

    Task<Event> AddEvent(Event newEvent);

    Task<Event> UpdateEvent(Event eventUpdated);

    Task<bool> DeleteEvent(int churchId, Guid guid);

    Task<int> CountEvents(int churchId);

    Task<IEnumerable<Event>> GetEventsByDateRange(int churchId, DateTime startDate, DateTime endDate);

    Task<Event> GetNextEvent(int churchId);

    Task<Event> ToggleStatus(int churchId, Guid guid, MeetingStatus status);
}
