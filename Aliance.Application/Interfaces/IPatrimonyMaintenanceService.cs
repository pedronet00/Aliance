using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Enums;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;
using Microsoft.AspNetCore.Http;

namespace Aliance.Application.Interfaces;

public interface IPatrimonyMaintenanceService
{
    Task<PagedResult<PatrimonyMaintenanceViewModel>> GetAllMaintenances(int pageNumber, int pageSize);
    Task<DomainNotificationsResult<PatrimonyMaintenanceViewModel>> GetMaintenanceByGuid(Guid guid);
    Task<DomainNotificationsResult<PatrimonyMaintenanceViewModel>> InsertMaintenance(PatrimonyMaintenanceDTO maintenance);
    Task<DomainNotificationsResult<PatrimonyMaintenanceViewModel>> UpdateMaintenance(PatrimonyMaintenanceDTO maintenance);
    Task<DomainNotificationsResult<PatrimonyMaintenanceViewModel>> DeleteMaintenance(Guid guid);
    Task<DomainNotificationsResult<PagedResult<PatrimonyMaintenanceViewModel>>> GetMaintenancesByPatrimonyGuid(Guid patrimonyGuid, int pageNumber, int pageSize);
    Task<DomainNotificationsResult<PatrimonyMaintenanceDocumentViewModel>> UploadDocumentAsync(Guid maintenanceGuid, IFormFile file);
    Task<IEnumerable<PatrimonyMaintenanceDocumentViewModel>> GetDocumentsByMaintenance(Guid maintenanceGuid);
    Task<DomainNotificationsResult<PatrimonyMaintenanceViewModel>> ToggleStatus(Guid maintenanceGuid, PatrimonyMaintenanceStatus status);
}
