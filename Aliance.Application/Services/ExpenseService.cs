using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Application.ViewModels;
using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Services;

public class ExpenseService : IExpenseService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExpenseRepository _repo;
    private readonly IUserContextService _userContext;

    public ExpenseService(IMapper mapper, IUnitOfWork unitOfWork, IExpenseRepository repo, IUserContextService userContext)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repo = repo;
        _userContext = userContext;
    }

    public async Task<DomainNotificationsResult<ExpenseViewModel>> DeleteExpense(Guid guid)
    {
        var result = new DomainNotificationsResult<ExpenseViewModel>();
        var churchId = _userContext.GetChurchId();

        var expense = await _repo.GetExpenseByGuid(churchId, guid);
        if (expense is null)
        {
            result.Notifications.Add("Entrada não existe.");
            return result;
        }

        await _repo.DeleteExpense(churchId, expense);
        await _unitOfWork.Commit();

        result.Result = _mapper.Map<ExpenseViewModel>(expense);
        return result;
    }

    public async Task<PagedResult<ExpenseViewModel>> GetAllExpenses(int pageNumber, int pageSize)
    {
        var churchId = _userContext.GetChurchId();

        var expenses = await _repo.GetAllExpenses(churchId, pageNumber, pageSize);

        var expensesVMs = _mapper.Map<IEnumerable<ExpenseViewModel>>(expenses.Items);

        return new PagedResult<ExpenseViewModel>(
                expensesVMs,
                expenses.TotalCount,
                expenses.CurrentPage,
                expenses.PageSize
        );
    }

    public async Task<DomainNotificationsResult<ExpenseViewModel>> GetExpenseByGuid(Guid guid)
    {
        var result = new DomainNotificationsResult<ExpenseViewModel>();
        var churchId = _userContext.GetChurchId();

        var expense = await _repo.GetExpenseByGuid(churchId, guid);
        if (expense is null)
        {
            result.Notifications.Add("Entrada não encontrada.");
            return result;
        }

        result.Result = _mapper.Map<ExpenseViewModel>(expense);
        return result;
    }

    public async Task<DomainNotificationsResult<IEnumerable<ExpenseViewModel>>> GetExpensesByCategory(FinancialExpenseCategory category)
    {
        var result = new DomainNotificationsResult<IEnumerable<ExpenseViewModel>>();
        var churchId = _userContext.GetChurchId();

        var expenses = await _repo.GetExpensesByCategory(churchId, category);
        if (!expenses.Any())
        {
            result.Notifications.Add("Nenhuma entrada encontrada para esta categoria.");
            return result;
        }

        result.Result = _mapper.Map<IEnumerable<ExpenseViewModel>>(expenses);
        return result;
    }

    public async Task<DomainNotificationsResult<IEnumerable<ExpenseMonthlyTotalViewModel>>> GetMonthlyTotals(int year)
    {
        var result = new DomainNotificationsResult<IEnumerable<ExpenseMonthlyTotalViewModel>>();
        var churchId = _userContext.GetChurchId();

        var totals = await _repo.GetMonthlyTotals(churchId, year);
        if (!totals.Any())
        {
            result.Notifications.Add("Nenhum registro de entrada encontrado para este ano.");
            return result;
        }

        result.Result = totals
            .Select(t => new ExpenseMonthlyTotalViewModel
            {
                Month = int.TryParse(t.Key, out var month) ? month : 0, // conversão segura
                Total = t.Value
            })
            .ToList();

        return result;
    }


    public async Task<DomainNotificationsResult<ExpenseRangeTotalViewModel>> GetTotalByDateRange(DateTime start, DateTime end)
    {
        var result = new DomainNotificationsResult<ExpenseRangeTotalViewModel>();
        var churchId = _userContext.GetChurchId();

        if (start > end)
        {
            result.Notifications.Add("Data inicial não pode ser maior que a data final.");
            return result;
        }

        var total = await _repo.GetTotalByDateRange(churchId, start, end);
        result.Result = _mapper.Map<ExpenseRangeTotalViewModel>(total);

        return result;
    }

    public async Task<DomainNotificationsResult<ExpenseViewModel>> InsertExpense(ExpenseDTO expenseDTO)
    {
        var result = new DomainNotificationsResult<ExpenseViewModel>();
        var churchId = _userContext.GetChurchId();

        var entity = _mapper.Map<Expense>(expenseDTO);

        await _repo.InsertExpense(entity);
        await _unitOfWork.Commit();

        result.Result = _mapper.Map<ExpenseViewModel>(entity);
        return result;
    }

    public async Task<DomainNotificationsResult<ExpenseViewModel>> UpdateExpense(ExpenseDTO expenseDTO)
    {
        var result = new DomainNotificationsResult<ExpenseViewModel>();
        var churchId = _userContext.GetChurchId();

        var existing = await _repo.GetExpenseByGuid(churchId, expenseDTO.Guid);
        if (existing is null)
        {
            result.Notifications.Add("Entrada não encontrada para atualização.");
            return result;
        }

        _mapper.Map(expenseDTO, existing);

        await _repo.UpdateExpense(churchId, existing);
        await _unitOfWork.Commit();

        result.Result = _mapper.Map<ExpenseViewModel>(existing);
        return result;
    }
}
