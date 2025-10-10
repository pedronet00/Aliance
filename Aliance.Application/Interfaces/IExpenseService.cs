using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Application.ViewModels;
using Aliance.Domain.Enums;
using Aliance.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface IExpenseService
{
    Task<IEnumerable<ExpenseViewModel>> GetAllExpenses();

    Task<DomainNotificationsResult<ExpenseViewModel>> GetExpenseByGuid(Guid guid);

    Task<DomainNotificationsResult<IEnumerable<ExpenseViewModel>>> GetExpensesByCategory(FinancialExpenseCategory category);

    Task<DomainNotificationsResult<ExpenseViewModel>> InsertExpense(ExpenseDTO expense);

    Task<DomainNotificationsResult<ExpenseViewModel>> UpdateExpense(ExpenseDTO expense);

    Task<DomainNotificationsResult<ExpenseViewModel>> DeleteExpense(Guid guid);

    Task<DomainNotificationsResult<ExpenseRangeTotalViewModel>> GetTotalByDateRange(DateTime start, DateTime end);

    Task<DomainNotificationsResult<IEnumerable<ExpenseMonthlyTotalViewModel>>> GetMonthlyTotals(int year);
}
