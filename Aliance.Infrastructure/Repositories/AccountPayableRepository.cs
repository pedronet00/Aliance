using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
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

public class AccountPayableRepository : IAccountPayableRepository
{
    private readonly AppDbContext _context;

    public AccountPayableRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AccountPayable> AddAsync(AccountPayable accountPayable)
    {
        if(accountPayable is null)
            throw new ArgumentNullException(nameof(accountPayable), "Account payable cannot be null.");

        await _context.AccountPayable.AddAsync(accountPayable);

        return accountPayable;

    }

    public async Task<bool> DeleteAsync(int churchId, Guid guid)
    {
        var accountPayable = await GetByGuidAsync(churchId, guid);

        if (accountPayable == null)
            return false;

        _context.AccountPayable.Remove(accountPayable);

        return true;
    }

    public async Task<PagedResult<AccountPayable>> GetAllAsync(int churchId, int pageNumber, int pageSize)
    {
        var query = _context.AccountPayable
            .Where(ap => ap.CostCenter.ChurchId == churchId)
            .OrderByDescending(ap => ap.DueDate)
            .Include(a => a.CostCenter)
            .AsNoTracking();

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();     

        var accountPayables = new PagedResult<AccountPayable>(items, totalCount, pageNumber, pageSize);

        return accountPayables;
    }

    public async Task<AccountPayable?> GetByGuidAsync(int churchId,Guid guid)
    {
        
        var accountPayable = await _context.AccountPayable
            .AsNoTracking()
            .FirstOrDefaultAsync(ap => ap.Guid == guid && ap.CostCenter.ChurchId == churchId);

        if (accountPayable is null)
            return null;

        return accountPayable;
    }

    public async Task<AccountPayable> ToggleStatus(int churchId, Guid guid, AccountStatus status)
    {
        var accountPayable = await _context.AccountPayable
            .FirstOrDefaultAsync(ap => ap.CostCenter.ChurchId == churchId && ap.Guid == guid);

        switch (status) 
        {
            case AccountStatus.Atrasada:
                accountPayable!.AccountStatus = AccountStatus.Atrasada;
                break;
            case AccountStatus.Paga:
                accountPayable!.AccountStatus = AccountStatus.Paga;
                break;
            case AccountStatus.Cancelada:
                accountPayable!.AccountStatus = AccountStatus.Cancelada;
                break;
            case AccountStatus.Parcial:
                accountPayable!.AccountStatus = AccountStatus.Parcial;
                break;
        }

        _context.Update(accountPayable);

        return accountPayable;
            
    }

    public async Task<AccountPayable> UpdateAsync(int churchId,AccountPayable accountPayable)
    {
        var existingAccountPayable = await _context.AccountPayable
            .FirstOrDefaultAsync(x => x.Id == accountPayable.Id && x.CostCenter.ChurchId == churchId);

        if (existingAccountPayable == null)
            throw new KeyNotFoundException($"Account payable with ID {accountPayable.Id} not found.");

        _context.Entry(existingAccountPayable).CurrentValues.SetValues(accountPayable);

        _context.AccountPayable.Update(existingAccountPayable); 

        return existingAccountPayable;
    }

}
