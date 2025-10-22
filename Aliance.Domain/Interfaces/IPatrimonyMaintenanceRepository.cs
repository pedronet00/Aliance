using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Pagination;

namespace Aliance.Domain.Interfaces;

public interface IPatrimonyMaintenanceRepository
{
    Task<PagedResult<PatrimonyMaintenance>> GetAllMaintenances(int churchId, int pageNumber, int pageSize);

    Task<PatrimonyMaintenance> GetMaintenanceByGuid(int churchId, Guid guid);

    Task<PatrimonyMaintenance> InsertMaintenance(PatrimonyMaintenance maintenance);

    Task<PatrimonyMaintenance> UpdateMaintenance(int churchId, PatrimonyMaintenance maintenance);

    Task<PatrimonyMaintenance> DeleteMaintenance(int churchId, Guid guid);

    Task<PagedResult<PatrimonyMaintenance>> GetMaintenancesByPatrimonyGuid(int churchId, Guid patrimonyGuid, int pageNumber, int pageSize);

    Task<bool> MaintenanceAlreadyExists(DateTime maintenanceDate, int patrimonyId);

    Task<PatrimonyMaintenance> GetByGuidWithDocumentsAsync(int churchId, Guid guid);

    Task<PatrimonyMaintenance> ToggleStatus(int churchId, Guid guid, PatrimonyMaintenanceStatus status);
}
