using Aliance.Application.ViewModel;
using Aliance.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface ICalendarActivitiesService
{
    Task<DomainNotificationsResult<List<CalendarItemViewModel>>> GetCalendarItems();

}
