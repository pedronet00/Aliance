using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Pagination;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

    public async Task<PagedResult<PatrimonyMaintenance>> GetAllMaintenances(int churchId, int pageNumber, int pageSize)
    {
        var query = _context.PatrimonyMaintenance
            .Include(m => m.Patrimony)
            .Where(m => m.Patrimony.ChurchId == churchId);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<PatrimonyMaintenance>(items, totalCount, pageNumber, pageSize);
    }

    public async Task<Domain.Entities.PatrimonyMaintenance> GetMaintenanceByGuid(int churchId, Guid guid)
    {
        var maintenance = await _context.PatrimonyMaintenance
            .Include(m => m.Patrimony)
            .FirstOrDefaultAsync(m => m.Guid == guid && m.Patrimony.ChurchId == churchId);

        return maintenance;
    }

    public async Task<PagedResult<PatrimonyMaintenance>> GetMaintenancesByPatrimonyGuid(int churchId, Guid patrimonyGuid, int pageNumber, int pageSize)
    {
        var query = _context.PatrimonyMaintenance
            .Include(m => m.Patrimony)
            .Where(m => m.Patrimony.Guid == patrimonyGuid && m.Patrimony.ChurchId == churchId);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<PatrimonyMaintenance>(items, totalCount, pageNumber, pageSize);
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
