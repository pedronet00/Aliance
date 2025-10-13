using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Enums;
using Aliance.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface IEventService
{
    Task<DomainNotificationsResult<IEnumerable<EventViewModel>>> GetEvents();

    Task<DomainNotificationsResult<EventViewModel>> GetEventByGuid(Guid guid);

    Task<DomainNotificationsResult<EventViewModel>> AddEvent(EventDTO newEvent);

    Task<DomainNotificationsResult<EventViewModel>> UpdateEvent(EventDTO eventUpdated);

    Task<DomainNotificationsResult<bool>> DeleteEvent(Guid guid);

    Task<DomainNotificationsResult<IEnumerable<EventViewModel>>> GetEventsByDateRange(DateTime startDate, DateTime endDate);

    Task<DomainNotificationsResult<EventViewModel>> GetNextEvent();

    Task<DomainNotificationsResult<EventViewModel>> ToggleStatus(Guid guid, MeetingStatus status);
}
