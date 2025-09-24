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

    public async Task<bool> DeleteAsync(int id)
    {
        var accountPayable = await GetByIdAsync(id);

        if (accountPayable == null)
            return false;

        _context.AccountPayable.Remove(accountPayable);

        return true;
    }

    public async Task<IEnumerable<AccountPayable>> GetAllAsync()
    {
        var accountPayables = await _context.AccountPayable
            .Include(a => a.CostCenter)
            .AsNoTracking()
            .ToListAsync();

        return accountPayables;
    }

    public async Task<AccountPayable?> GetByIdAsync(int id)
    {
        
        var accountPayable = await _context.AccountPayable
            .AsNoTracking()
            .FirstOrDefaultAsync(ap => ap.Id == id);

        if (accountPayable is null)
            return null;

        return accountPayable;
    }

    public async Task<AccountPayable> UpdateAsync(AccountPayable accountPayable)
    {
        var existingAccountPayable = await _context.AccountPayable
            .FirstOrDefaultAsync(x => x.Id == accountPayable.Id);

        if (existingAccountPayable == null)
            throw new KeyNotFoundException($"Account payable with ID {accountPayable.Id} not found.");

        _context.Entry(existingAccountPayable).CurrentValues.SetValues(accountPayable);

        _context.AccountPayable.Update(existingAccountPayable); 

        return existingAccountPayable;
    }

}
