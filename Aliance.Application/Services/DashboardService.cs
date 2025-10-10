using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Application.ViewModels;
using Aliance.Domain.Notifications;
using System.Threading.Tasks;

namespace Aliance.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IIncomeService _incomeService;
    private readonly IExpenseService _expenseService;
    private readonly IUserService _userService;
    private readonly IPatrimonyService _patrimonyService;

    public DashboardService(IIncomeService incomeService, IExpenseService expenseService, IUserService userService = null, IPatrimonyService patrimonyService = null)
    {
        _incomeService = incomeService;
        _expenseService = expenseService;
        _userService = userService;
        _patrimonyService = patrimonyService;
    }

    public async Task<DomainNotificationsResult<DashboardViewModel>> GetDashboardData(int year)
    {
        var result = new DomainNotificationsResult<DashboardViewModel>();

        var incomeResult = await _incomeService.GetMonthlyTotals(year);
        var expenseResult = await _expenseService.GetMonthlyTotals(year);
        var totalUsers = await _userService.CountUsers();
        var totalPatrimonies = await _patrimonyService.PatrimoniesCount();

        if (incomeResult.Notifications.Any())
            result.Notifications.AddRange(incomeResult.Notifications);

        if (expenseResult.Notifications.Any())
            result.Notifications.AddRange(expenseResult.Notifications);

        result.Result = new DashboardViewModel
        {
            IncomeTotals = incomeResult.Result ?? new List<IncomeMonthlyTotalViewModel>(),
            ExpenseTotals = expenseResult.Result ?? new List<ExpenseMonthlyTotalViewModel>(),
            TotalUsers = totalUsers.Result,
            TotalPatrimonies = totalPatrimonies.Result
        };

        return result;
    }
}
