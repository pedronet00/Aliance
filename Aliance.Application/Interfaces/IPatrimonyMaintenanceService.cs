using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Enums;
using Aliance.Domain.Notifications;
using Microsoft.AspNetCore.Http;

namespace Aliance.Application.Interfaces;

public interface IPatrimonyMaintenanceService
{
    Task<IEnumerable<PatrimonyMaintenanceViewModel>> GetAllMaintenances();
    Task<DomainNotificationsResult<PatrimonyMaintenanceViewModel>> GetMaintenanceByGuid(Guid guid);
    Task<DomainNotificationsResult<PatrimonyMaintenanceViewModel>> InsertMaintenance(PatrimonyMaintenanceDTO maintenance);
    Task<DomainNotificationsResult<PatrimonyMaintenanceViewModel>> UpdateMaintenance(PatrimonyMaintenanceDTO maintenance);
    Task<DomainNotificationsResult<PatrimonyMaintenanceViewModel>> DeleteMaintenance(Guid guid);
    Task<DomainNotificationsResult<IEnumerable<PatrimonyMaintenanceViewModel>>> GetMaintenancesByPatrimonyGuid(Guid patrimonyGuid);
    Task<DomainNotificationsResult<PatrimonyMaintenanceDocumentViewModel>> UploadDocumentAsync(Guid maintenanceGuid, IFormFile file);
    Task<IEnumerable<PatrimonyMaintenanceDocumentViewModel>> GetDocumentsByMaintenance(Guid maintenanceGuid);
    Task<DomainNotificationsResult<PatrimonyMaintenanceViewModel>> ToggleStatus(Guid maintenanceGuid, PatrimonyMaintenanceStatus status);
}
