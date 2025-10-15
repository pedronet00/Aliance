using Aliance.Domain.Entities;

namespace Aliance.Domain.Interfaces;

public interface IBudgetRepository
{
    Task<IEnumerable<Budget>> GetAllBudgetsAsync(int churchId);

    Task<Budget?> GetBudgetByGuidAsync(int churchId, Guid guid);

    Task<Budget> AddBudgetAsync(Budget budget);

    Task<Budget> UpdateBudgetAsync(int churchId, Budget budget);

    Task<bool> DeleteBudgetAsync(int churchId, Guid guid);

    Task<bool> ApproveBudget(int churchId, Guid guid);

    Task<bool> RejectBudget(int churchId, Guid guid);

    Task<bool> ContestBudget(int churchId, Guid guid);

    Task<bool> EndBudget(int churchId, Guid guid);

    Task<int> CountBudgets(int churchId);
}
