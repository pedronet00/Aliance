using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Interfaces;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Aliance.Infrastructure.Repositories;

public class PatrimonyMaintenanceRepository : IPatrimonyMaintenanceRepository
{

    private readonly AppDbContext _context;

    public PatrimonyMaintenanceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.PatrimonyMaintenance> DeleteMaintenance(int churchId, Guid guid)
    {
        var maintenance = await _context.PatrimonyMaintenance
            .FirstOrDefaultAsync(m => m.Guid == guid && m.Patrimony.ChurchId == churchId);

        _context.PatrimonyMaintenance.Remove(maintenance);

        return maintenance;
    }

    public async Task<IEnumerable<Domain.Entities.PatrimonyMaintenance>> GetAllMaintenances(int churchId)
    {
        var maintenances = await _context.PatrimonyMaintenance
            .Include(m => m.Patrimony)
            .Where(m => m.Patrimony.ChurchId == churchId)
            .ToListAsync();

        return maintenances;
    }

    public async Task<Domain.Entities.PatrimonyMaintenance> GetMaintenanceByGuid(int churchId, Guid guid)
    {
        var maintenance = await _context.PatrimonyMaintenance
            .Include(m => m.Patrimony)
            .FirstOrDefaultAsync(m => m.Guid == guid && m.Patrimony.ChurchId == churchId);

        return maintenance;
    }

    public async Task<IEnumerable<Domain.Entities.PatrimonyMaintenance>> GetMaintenancesByPatrimonyGuid(int churchId, Guid patrimonyGuid)
    {
        var maintenances = await _context.PatrimonyMaintenance
            .Include(m => m.Patrimony)
            .Where(m => m.Patrimony.Guid == patrimonyGuid && m.Patrimony.ChurchId == churchId)
            .ToListAsync();

        return maintenances;
    }

    public async Task<Domain.Entities.PatrimonyMaintenance> InsertMaintenance(Domain.Entities.PatrimonyMaintenance maintenance)
    {
        await _context.PatrimonyMaintenance.AddAsync(maintenance);

        return maintenance;
    }

    public async Task<bool> MaintenanceAlreadyExists(DateTime maintenanceDate, int patrimonyId)
    {
        var dateOnly = maintenanceDate.Date;

        var maintenanceAlreadyExists = await _context.PatrimonyMaintenance
            .Where(m => m.MaintenanceDate.Date == dateOnly && m.PatrimonyId == patrimonyId)
            .AnyAsync();

        return maintenanceAlreadyExists;
    }


    public async Task<Domain.Entities.PatrimonyMaintenance> UpdateMaintenance(int churchId, Domain.Entities.PatrimonyMaintenance maintenance)
    {
        _context.PatrimonyMaintenance.Update(maintenance);

        return maintenance;
    }

    public async Task<PatrimonyMaintenance> GetByGuidWithDocumentsAsync(int churchId, Guid guid)
    {
        var documents = await _context.PatrimonyMaintenance
            .Include(p => p.Documents) // carrega os documentos vinculados
            .FirstOrDefaultAsync(p => p.Guid == guid && p.Patrimony.ChurchId == churchId);

        return documents!;
    }

    public async Task<PatrimonyMaintenance> ToggleStatus(int churchId, Guid guid, PatrimonyMaintenanceStatus status)
    {
        var maintenance = await _context.PatrimonyMaintenance
            .Where(pm => pm.Patrimony.ChurchId == churchId && pm.Guid == guid)
            .FirstOrDefaultAsync();

        maintenance.Status = status;

        _context.PatrimonyMaintenance.Update(maintenance);

        return maintenance;
    }
}
