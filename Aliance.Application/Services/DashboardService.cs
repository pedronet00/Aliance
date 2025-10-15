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
    private readonly IEventService _eventService;
    private readonly IBudgetService _budgetService;

    public DashboardService(IIncomeService incomeService, IExpenseService expenseService, IUserService userService = null, IPatrimonyService patrimonyService = null, IEventService eventService = null, IBudgetService budgetService = null)
    {
        _incomeService = incomeService;
        _expenseService = expenseService;
        _userService = userService;
        _patrimonyService = patrimonyService;
        _eventService = eventService;
        _budgetService = budgetService;
    }

    public async Task<DomainNotificationsResult<DashboardViewModel>> GetDashboardData(int year)
    {
        var result = new DomainNotificationsResult<DashboardViewModel>();

        var incomeResult = await _incomeService.GetMonthlyTotals(year);
        var expenseResult = await _expenseService.GetMonthlyTotals(year);
        var totalUsers = await _userService.CountUsers();
        var totalPatrimonies = await _patrimonyService.PatrimoniesCount();
        var totalEvents = await _eventService.CountEvents();
        var totalBudgets = await _budgetService.CountBudgets();

        if (incomeResult.Notifications.Any())
            result.Notifications.AddRange(incomeResult.Notifications);

        if (expenseResult.Notifications.Any())
            result.Notifications.AddRange(expenseResult.Notifications);

        result.Result = new DashboardViewModel
        {
            IncomeTotals = incomeResult.Result ?? new List<IncomeMonthlyTotalViewModel>(),
            ExpenseTotals = expenseResult.Result ?? new List<ExpenseMonthlyTotalViewModel>(),
            TotalUsers = totalUsers.Result,
            TotalPatrimonies = totalPatrimonies.Result,
            TotalEvents = totalEvents.Result,
            TotalBudgets = totalBudgets.Result
        };

        return result;
    }
}
