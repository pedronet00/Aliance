using Aliance.Domain.Entities;
using Aliance.Domain.Enums;

namespace Aliance.Domain.Interfaces;

public interface IPatrimonyMaintenanceRepository
{
    Task<IEnumerable<PatrimonyMaintenance>> GetAllMaintenances(int churchId);

    Task<PatrimonyMaintenance> GetMaintenanceByGuid(int churchId, Guid guid);

    Task<PatrimonyMaintenance> InsertMaintenance(PatrimonyMaintenance maintenance);

    Task<PatrimonyMaintenance> UpdateMaintenance(int churchId, PatrimonyMaintenance maintenance);

    Task<PatrimonyMaintenance> DeleteMaintenance(int churchId, Guid guid);

    Task<IEnumerable<PatrimonyMaintenance>> GetMaintenancesByPatrimonyGuid(int churchId, Guid patrimonyGuid);

    Task<bool> MaintenanceAlreadyExists(DateTime maintenanceDate, int patrimonyId);

    Task<PatrimonyMaintenance> GetByGuidWithDocumentsAsync(int churchId, Guid guid);

    Task<PatrimonyMaintenance> ToggleStatus(int churchId, Guid guid, PatrimonyMaintenanceStatus status);
}
