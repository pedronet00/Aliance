using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aliance.Application.Services
{
    public class CalendarActivitiesService : ICalendarActivitiesService
    {
        private readonly IServiceService _serviceService;
        private readonly IEventService _eventService;

        public CalendarActivitiesService(IServiceService serviceService, IEventService eventService)
        {
            _serviceService = serviceService;
            _eventService = eventService;
        }

        public async Task<DomainNotificationsResult<List<CalendarItemViewModel>>> GetCalendarItems()
        {
            var result = new DomainNotificationsResult<List<CalendarItemViewModel>>();
            var calendarItems = new List<CalendarItemViewModel>();

            // Buscar eventos e cultos
            var events = await _eventService.GetEvents(1, 10000);
            var services = await _serviceService.GetServices(1, 10000);

            // Mapeia eventos
            if (events?.Result?.Items != null)
            {
                calendarItems.AddRange(events.Result.Items.Select(e => new CalendarItemViewModel
                {
                    Title = e.Name,
                    Date = e.Date,
                    Type = "Evento",
                    ReferenceId = e.Guid
                }));
            }

            // Mapeia cultos
            if (services?.Result?.Items != null)
            {
                calendarItems.AddRange(services.Result.Items.Select(s => new CalendarItemViewModel
                {
                    Title = $"Culto em {s.LocationName}",
                    Date = s.Date,
                    Type = "Culto",
                    ReferenceId = s.Guid
                }));
            }

            // Define resultado
            result.Result = calendarItems;

            return result;
        }
    }
}
