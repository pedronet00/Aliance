using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface IExpenseRepository
{
    Task<PagedResult<Expense>> GetAllExpenses(int churchId, int pageNumber, int pageSize);

    Task<Expense> GetExpenseByGuid(int churchId, Guid guid);

    Task<IEnumerable<Expense>> GetExpensesByCategory(int churchId, FinancialExpenseCategory category);

    Task<Expense> InsertExpense(Expense expense);

    Task<Expense> UpdateExpense(int churchId, Expense expense);

    Task<Expense> DeleteExpense(int churchId, Expense expense);

    Task<decimal> GetTotalByDateRange(int churchId, DateTime start, DateTime end);

    Task<Dictionary<string, decimal>> GetMonthlyTotals(int churchId, int year);
}
