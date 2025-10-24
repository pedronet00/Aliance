using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Application.ViewModels;
using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface IIncomeService
{
    Task<PagedResult<IncomeViewModel>> GetAllIncomes(int pageNumber, int pageSize);

    Task<DomainNotificationsResult<IncomeViewModel>> GetIncomeByGuid(Guid guid);

    Task<DomainNotificationsResult<IEnumerable<IncomeViewModel>>> GetIncomesByCategory(FinancialIncomingCategory category);

    Task<DomainNotificationsResult<IncomeViewModel>> InsertIncome(IncomeDTO income);

    Task<DomainNotificationsResult<IncomeViewModel>> UpdateIncome(IncomeDTO income);

    Task<DomainNotificationsResult<IncomeViewModel>> DeleteIncome(Guid guid);

    Task<DomainNotificationsResult<IncomeRangeTotalViewModel>> GetTotalByDateRange(DateTime start, DateTime end);

    Task<DomainNotificationsResult<IEnumerable<IncomeMonthlyTotalViewModel>>> GetMonthlyTotals(int year);
}

