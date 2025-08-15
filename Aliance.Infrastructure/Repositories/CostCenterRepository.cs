using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.Repositories;

public class CostCenterRepository : ICostCenterRepository
{

    private readonly AppDbContext _context;

    public CostCenterRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CostCenter> Add(CostCenter costCenter)
    {
        if(costCenter is null)
            throw new ArgumentNullException(nameof(costCenter), "CostCenter cannot be null");

        await _context.CostCenter.AddAsync(costCenter);

        return costCenter;
    }

    public async Task<bool> Delete(int id)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than zero");

        var costCenter = await _context.CostCenter.FindAsync(id);

        if (costCenter is null)
            throw new KeyNotFoundException($"CostCenter with id {id} not found");

        var result = _context.CostCenter.Remove(costCenter);

        return true;
    }

    public async Task<IEnumerable<CostCenter>> GetAllCenters()
    {
        var centers = await _context.CostCenter.AsNoTracking().ToListAsync();

        return centers;
    }

    public async Task<CostCenter> GetById(int id)
    {
        var center = await _context.CostCenter.FindAsync(id);

        if (center is null)
            throw new KeyNotFoundException($"CostCenter with id {id} not found");

        return center;
    }

    public async Task<CostCenter> Update(CostCenter costCenter)
    {
        if (costCenter is null)
            throw new ArgumentNullException(nameof(costCenter), "CostCenter cannot be null");

        var existingCenter = await _context.CostCenter.AsNoTracking().FirstOrDefaultAsync(c => c.Id == costCenter.Id);

        if (existingCenter is null)
            throw new KeyNotFoundException($"CostCenter with id {costCenter.Id} not found");

         _context.CostCenter.Update(existingCenter);

        return costCenter;
    }
}
