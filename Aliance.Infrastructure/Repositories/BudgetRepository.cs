using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Interfaces;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Aliance.Infrastructure.Repositories;

public class BudgetRepository : IBudgetRepository
{
    private readonly AppDbContext _context;

    public BudgetRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Budget> AddBudgetAsync(Budget budget)
    {
        await _context.Budget.AddAsync(budget);

        return budget;
    }

    public async Task<bool> ApproveBudget(int churchId, Guid guid)
    {
        await _context.Budget
        .Where(c => c.Guid == guid && c.CostCenter.ChurchId == churchId)
        .ExecuteUpdateAsync(setters => setters.SetProperty(b => b.Status, BudgetStatus.Aprovado));

        return true;
    }

    public async Task<bool> ContestBudget(int churchId, Guid guid)
    {
        await _context.Budget
        .Where(c => c.Guid == guid && c.CostCenter.ChurchId == churchId)
        .ExecuteUpdateAsync(setters => setters.SetProperty(b => b.Status, BudgetStatus.Contestado));


        return true;
    }

    public async Task<int> CountBudgets(int churchId)
    {
        var totalBudgets = await _context.Budget
            .Where(b => b.CostCenter.ChurchId == churchId)
            .CountAsync();

        return totalBudgets;
    }

    public async Task<bool> DeleteBudgetAsync(int churchId, Guid guid)
    {
        var budget = await this.GetBudgetByGuidAsync(churchId, guid);

        _context.Budget.Remove(budget!);

        return true;
    }

    public async Task<bool> EndBudget(int churchId, Guid guid)
    {
        await _context.Budget
        .Where(c => c.Guid == guid && c.CostCenter.ChurchId == churchId)
        .ExecuteUpdateAsync(setters => setters.SetProperty(b => b.Status, BudgetStatus.Encerrado));


        return true;
    }

    public async Task<IEnumerable<Budget>> GetAllBudgetsAsync(int churchId)
    {
        return await _context.Budget.Where(c => c.CostCenter.ChurchId == churchId)
            .Include(b => b.CostCenter)
            .ToListAsync();
    }

    public async Task<Budget?> GetBudgetByGuidAsync(int churchId, Guid guid)
    {
        return await _context.Budget.Where(c => c.Guid == guid && c.CostCenter.ChurchId == churchId).FirstOrDefaultAsync();
    }

    public async Task<bool> RejectBudget(int churchId, Guid guid)
    {
        await _context.Budget
        .Where(c => c.Guid == guid && c.CostCenter.ChurchId == churchId)
        .ExecuteUpdateAsync(setters => setters.SetProperty(b => b.Status, BudgetStatus.Rejeitado));


        return true;
    }

    public async Task<Budget> UpdateBudgetAsync(int churchId, Budget budget)
    {
        _context.Budget.Update(budget);

        return budget;
    }
}
