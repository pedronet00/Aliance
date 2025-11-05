using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Pagination;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Aliance.Infrastructure.Repositories;

public class AccountReceivableRepository : IAccountReceivableRepository
{
    private readonly AppDbContext _context;

    public AccountReceivableRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AccountReceivable> AddAsync(AccountReceivable accountReceivable)
    {
        if (accountReceivable is null)
            throw new ArgumentNullException(nameof(accountReceivable), "Account receivable cannot be null.");

        await _context.AccountReceivable.AddAsync(accountReceivable);

        return accountReceivable;

    }

    public async Task<bool> DeleteAsync(int churchId, Guid guid)
    {
        var accountReceivable = await GetByGuidAsync(churchId, guid);

        if (accountReceivable == null)
            return false;

        _context.AccountReceivable.Remove(accountReceivable);

        return true;
    }

    public async Task<PagedResult<AccountReceivable>> GetAllAsync(int churchId, int pageNumber, int pageSize)
    {
        var query = _context.AccountReceivable
            .Where(ap => ap.CostCenter.ChurchId == churchId)
            .Include(a => a.CostCenter)
            .OrderByDescending(ap => ap.DueDate)
            .AsNoTracking();

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<AccountReceivable>(
            items,
            totalCount,
            pageNumber,
            pageSize
        );
    }

    public async Task<AccountReceivable?> GetByGuidAsync(int churchId, Guid guid)
    {

        var accountReceivable = await _context.AccountReceivable
            .AsNoTracking()
            .FirstOrDefaultAsync(ap => ap.Guid == guid && ap.CostCenter.ChurchId == churchId);

        if (accountReceivable is null)
            return null;

        return accountReceivable;
    }

    public async Task<IEnumerable<AccountReceivable>> GetExpiringAccounts()
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);

        return await _context.AccountReceivable
            .Where(ap => ap.DueDate <= tomorrow && ap.AccountStatus == AccountStatus.Pendente)
            .ToListAsync();
    }

    public async Task<AccountReceivable> ToggleStatus(int churchId, Guid guid, AccountStatus status)
    {
        var accountReceivable = await _context.AccountReceivable
            .FirstOrDefaultAsync(ap => ap.CostCenter.ChurchId == churchId && ap.Guid == guid);

        switch (status)
        {
            case AccountStatus.Atrasada:
                accountReceivable!.AccountStatus = AccountStatus.Atrasada;
                break;
            case AccountStatus.Paga:
                accountReceivable!.AccountStatus = AccountStatus.Paga;
                break;
            case AccountStatus.Cancelada:
                accountReceivable!.AccountStatus = AccountStatus.Cancelada;
                break;
            case AccountStatus.Parcial:
                accountReceivable!.AccountStatus = AccountStatus.Parcial;
                break;
        }

        _context.Update(accountReceivable);

        return accountReceivable;

    }

    public async Task<AccountReceivable> UpdateAsync(int churchId, AccountReceivable accountReceivable)
    {
        var existingAccountReceivable = await _context.AccountReceivable
            .FirstOrDefaultAsync(x => x.Id == accountReceivable.Id && x.CostCenter.ChurchId == churchId);

        if (existingAccountReceivable == null)
            throw new KeyNotFoundException($"Account receivable with ID {accountReceivable.Id} not found.");

        _context.Entry(existingAccountReceivable).CurrentValues.SetValues(accountReceivable);

        _context.AccountReceivable.Update(existingAccountReceivable);

        return existingAccountReceivable;
    }
}
