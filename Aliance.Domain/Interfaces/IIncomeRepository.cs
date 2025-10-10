using Aliance.Domain.Entities;
using Aliance.Domain.Enums;

namespace Aliance.Domain.Interfaces;

public interface IIncomeRepository
{
    Task<IEnumerable<Income>> GetAllIncomes(int churchId);

    Task<Income> GetIncomeByGuid(int churchId, Guid guid);

    Task<IEnumerable<Income>> GetIncomesByCategory(int churchId, FinancialIncomingCategory category);

    Task<Income> InsertIncome(Income income);

    Task<Income> UpdateIncome(int churchId, Income income);

    Task<Income> DeleteIncome(int churchId, Income income);

    Task<decimal> GetTotalByDateRange(int churchId, DateTime start, DateTime end);

    Task<Dictionary<string, decimal>> GetMonthlyTotals(int churchId, int year);

}
