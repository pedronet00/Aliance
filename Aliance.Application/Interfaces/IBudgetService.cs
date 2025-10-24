using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface IBudgetService
{
    Task<PagedResult<BudgetViewModel>> GetAllBudgetsAsync(int pageNumber, int pageSize);

    Task<DomainNotificationsResult<BudgetViewModel>> GetBudgetByIdAsync(Guid guid);

    Task<DomainNotificationsResult<BudgetViewModel>> AddBudgetAsync(BudgetDTO budget);

    Task<DomainNotificationsResult<BudgetViewModel>> UpdateBudgetAsync(BudgetDTO budget);

    Task<DomainNotificationsResult<bool>> DeleteBudgetAsync(Guid guid);

    Task<DomainNotificationsResult<BudgetViewModel>> ApproveBudget(Guid guid);

    Task<DomainNotificationsResult<BudgetViewModel>> RejectBudget(Guid guid);

    Task<DomainNotificationsResult<BudgetViewModel>> ContestBudget(Guid guid);

    Task<DomainNotificationsResult<BudgetViewModel>> EndBudget(Guid guid);

    Task<DomainNotificationsResult<int>> CountBudgets();
}
