using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Pagination;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Aliance.Infrastructure.Repositories;

public class CostCenterRepository : ICostCenterRepository
{

    private readonly AppDbContext _context;

    public CostCenterRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CostCenter> Activate(int churchId, Guid guid)
    {
        var existingCenter = await _context.CostCenter
            .FirstOrDefaultAsync(c => c.Guid == guid && c.ChurchId == churchId);

        existingCenter.Status = true;
        _context.CostCenter.Update(existingCenter);

        return existingCenter;
    }

    public async Task<CostCenter> Add(CostCenter costCenter)
    {

        await _context.CostCenter.AddAsync(costCenter);

        return costCenter;
    }

    public async Task<CostCenter> Deactivate(int churchId, Guid guid)
    {
        var existingCenter = await _context.CostCenter
            .FirstOrDefaultAsync(c => c.Guid == guid && c.ChurchId == churchId);

        existingCenter.Status = false;
        _context.CostCenter.Update(existingCenter);

        return existingCenter;
    }

    public async Task<bool> Delete(int churchId, Guid guid)
    {
        var costCenter = await _context.CostCenter.Where(c => c.ChurchId == churchId && c.Guid == guid).FirstOrDefaultAsync();

        var result = _context.CostCenter.Remove(costCenter);

        return true;
    }

    public async Task<PagedResult<CostCenter>> GetAllCenters(int churchId, int pageNumber, int pageSize)
    {
        var query = _context.CostCenter
            .Where(c => c.ChurchId == churchId)
            .Include(c => c.Department)
            .OrderBy(c => c.Name)
            .AsNoTracking();

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<CostCenter>(items, totalCount, pageNumber, pageSize);
    }


    public async Task<CostCenter> GetByGuid(int churchId, Guid guid)
    {
        var center = await _context.CostCenter
            .Include(c => c.Department)
            .Where(c => c.ChurchId == churchId && c.Guid == guid)
            .FirstOrDefaultAsync();

        return center;
    }


    public async Task<CostCenter> Update(int churchId, CostCenter costCenter)
    {
        _context.CostCenter.Update(costCenter);

        return costCenter;
    }
}
