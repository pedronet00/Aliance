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

public class TitheRepository : ITitheRepository
{
    private readonly AppDbContext _context;

    public TitheRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Tithe> AddTithe(Tithe tithe)
    {
        await _context.Tithe.AddAsync(tithe);

        return tithe;
    }

    public async Task<Tithe> DeleteTithe(int churchId, Guid guid)
    {
        var tithe = await _context.Tithe
            .FirstOrDefaultAsync(t => t.Guid == guid && t.ChurchId == churchId);

        _context.Tithe.Remove(tithe);

        return tithe;
    }

    public async Task<Tithe> GetTitheByGuid(int churchId, Guid guid)
    {
        var tithe = await _context.Tithe
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Guid == guid && t.ChurchId == churchId);

        return tithe;
    }

    public async Task<PagedResult<Tithe>> GetTithes(int churchId, int pageNumber, int pageSize)
    {
        var tithes = _context.Tithe
            .Include(t => t.User)
            .OrderByDescending(t => t.Date)
            .Where(t => t.ChurchId == churchId);

        var totalCount = await tithes.CountAsync();

        var items = await tithes
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Tithe>(items, totalCount, pageNumber, pageSize);
    }

    public async Task<IEnumerable<Tithe>> GetTithesByUser(int churchId, string userId)
    {
        var tithes = await _context.Tithe
            .Where(t => t.ChurchId == churchId && t.UserId == userId)
            .ToListAsync();

        return tithes;
    }

    public async Task<decimal> GetTotalTithes(int churchId)
    {
        var totalTithes = await _context.Tithe
            .Where(t => t.ChurchId == churchId)
            .SumAsync(t => t.Amount.Value);

        return totalTithes;
    }

    public async Task<decimal> GetTotalTithesByUser(int churchId, string userId)
    {
        var totalTithes = await _context.Tithe
            .Where(t => t.ChurchId == churchId && t.UserId == userId)
            .SumAsync(t => t.Amount.Value);

        return totalTithes;
    }

    public async Task<Tithe> UpdateTithe(Tithe tithe)
    {
        _context.Tithe.Update(tithe);

        return tithe;
    }
}
