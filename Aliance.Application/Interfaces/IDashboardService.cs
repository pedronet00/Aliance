using Aliance.Application.ViewModel;
using Aliance.Application.ViewModels;
using Aliance.Domain.Notifications;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface IDashboardService
{
    Task<DomainNotificationsResult<DashboardViewModel>> GetDashboardData(int year);
}
