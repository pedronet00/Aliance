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

namespace Aliance.Infrastructure.Repositories;

public class AutomaticAccountsRepository : IAutomaticAccountsRepository
{
    private readonly AppDbContext _context;

    public AutomaticAccountsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AutomaticAccounts> Delete(int id)
    {
        var automaticAccount = await _context.AutomaticAccounts
            .Where(aa => aa.Id == id)
            .FirstOrDefaultAsync();

        _context.Remove(automaticAccount);

        return automaticAccount;
    }

    public async Task<PagedResult<AutomaticAccounts>> GetAllPaged(int churchId, int pageNumber, int pageSize)
    {
        var query = _context.AutomaticAccounts
            .Where(aa => aa.ChurchId == churchId)
            .Include(aa => aa.CostCenter)
            .AsNoTracking();

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<AutomaticAccounts>(
            items,
            totalCount,
            pageNumber,
            pageSize
        );
    }

    public async Task<IEnumerable<AutomaticAccounts>> GetAll()
    {
        var accounts = await _context.AutomaticAccounts
            //.Where(aa => aa.ChurchId == churchId)
            .AsNoTracking()
            .ToListAsync();

        return accounts;
    }
    public async Task<AutomaticAccounts> GetByGuid(Guid guid)
    {
        var account = await _context.AutomaticAccounts
            .Where(aa => aa.Guid == guid)
            .FirstOrDefaultAsync();

        return account;
    }

    public async Task<AutomaticAccounts> GetById(int id)
    {
        var account = await _context.AutomaticAccounts
            .Where(aa => aa.Id == id)
            .FirstOrDefaultAsync();

        return account;
    }

    public async Task<AutomaticAccounts> Insert(AutomaticAccounts automaticAccount)
    {
        await _context.AutomaticAccounts.AddAsync(automaticAccount);

        return automaticAccount;
    }

    public async Task<AutomaticAccounts> Update(AutomaticAccounts automaticAccount)
    {
        _context.AutomaticAccounts.Update(automaticAccount);

        return automaticAccount;
    }
}
