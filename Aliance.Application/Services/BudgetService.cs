using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Enums;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Services;

public class BudgetService : IBudgetService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IBudgetRepository _repo;
    private readonly IUserContextService _userContext;

    public BudgetService(IUnitOfWork uow, IMapper mapper, IBudgetRepository repo, IUserContextService userContext)
    {
        _uow = uow;
        _mapper = mapper;
        _repo = repo;
        _userContext = userContext;
    }

    public async Task<DomainNotificationsResult<BudgetViewModel>> ApproveBudget(Guid guid)
    {
        var churchId = _userContext.GetChurchId();
        var result = new DomainNotificationsResult<BudgetViewModel>();

        var budget = await _repo.GetBudgetByGuidAsync(churchId, guid);

        if (budget == null)
            result.Notifications.Add("Orçamento não encontrado.");

        if(budget.Status == BudgetStatus.Aprovado)
            result.Notifications.Add("Orçamento já está aprovado.");

        if (result.Notifications.Any())
            return result;

        budget.Status = BudgetStatus.Aprovado;
        var updatedBudget = await _repo.UpdateBudgetAsync(churchId, budget);

        await _uow.Commit();

        result.Result = _mapper.Map<BudgetViewModel>(updatedBudget);

        return result;
    }

    public async Task<DomainNotificationsResult<BudgetViewModel>> AddBudgetAsync(BudgetDTO budget)
    {
        var result = new DomainNotificationsResult<BudgetViewModel>();

        if(budget.TotalAmount <= 0)
            result.Notifications.Add("O valor total do orçamento deve ser maior que zero.");

        if (result.Notifications.Any())
            return result;

        var budgetEntity = _mapper.Map<Domain.Entities.Budget>(budget);

        var addedBudget = await _repo.AddBudgetAsync(budgetEntity);

        await _uow.Commit();

        result.Result = _mapper.Map<BudgetViewModel>(addedBudget);

        return result;
    }

    public async Task<DomainNotificationsResult<bool>> DeleteBudgetAsync(Guid guid)
    {
        var churchId = _userContext.GetChurchId();

        var result = new DomainNotificationsResult<bool>();

        var budget = await _repo.GetBudgetByGuidAsync(churchId, guid);

        if (budget == null)
            result.Notifications.Add("Orçamento não encontrado.");

        if (result.Notifications.Any())
            return result;

        await _repo.DeleteBudgetAsync(churchId, guid);

        await _uow.Commit();

        result.Result = true;

        return result;
    }

    public async Task<PagedResult<BudgetViewModel>> GetAllBudgetsAsync(int pageNumber, int pageSize)
    {
        var churchId = _userContext.GetChurchId();

        var budgets = await _repo.GetAllBudgetsAsync(churchId, pageNumber, pageSize);

        var budgetsVMs =  _mapper.Map<IEnumerable<BudgetViewModel>>(budgets.Items);

        return new PagedResult<BudgetViewModel>(
            budgetsVMs,
            budgets.TotalCount,
            budgets.CurrentPage,
            budgets.PageSize
            );
    }

    public async Task<DomainNotificationsResult<BudgetViewModel>> GetBudgetByIdAsync(Guid guid)
    {
        var churchId = _userContext.GetChurchId();
        var result = new DomainNotificationsResult<BudgetViewModel>();

        var budget = await _repo.GetBudgetByGuidAsync(churchId, guid);

        if (budget == null)
            result.Notifications.Add("Orçamento não encontrado.");

        if (result.Notifications.Any())
            return result;

        result.Result = _mapper.Map<BudgetViewModel>(budget);

        return result;
    }

    public async Task<DomainNotificationsResult<BudgetViewModel>> UpdateBudgetAsync(BudgetDTO budget)
    {
        var churchId = _userContext.GetChurchId();
        var result = new DomainNotificationsResult<BudgetViewModel>();
        var budgetEntity = await _repo.GetBudgetByGuidAsync(churchId, budget.Guid);

        if (budgetEntity is null)
            result.Notifications.Add("Orçamento não encontrado.");

        if (budget.TotalAmount <= 0)
            result.Notifications.Add("O valor total do orçamento deve ser maior que zero.");

        if (result.Notifications.Any())
            return result;

        _mapper.Map(budget, budgetEntity);

        var updatedBudget = await _repo.UpdateBudgetAsync(churchId, budgetEntity);

        await _uow.Commit();

        result.Result = _mapper.Map<BudgetViewModel>(updatedBudget);

        return result;
    }


    public async Task<DomainNotificationsResult<BudgetViewModel>> RejectBudget(Guid guid)
    {
        var churchId = _userContext.GetChurchId();
        var result = new DomainNotificationsResult<BudgetViewModel>();

        var budget = await _repo.GetBudgetByGuidAsync(churchId, guid);

        if (budget == null)
            result.Notifications.Add("Orçamento não encontrado.");

        if (budget.Status == BudgetStatus.Rejeitado)
            result.Notifications.Add("Orçamento já está rejeitado.");

        if (result.Notifications.Any())
            return result;

        budget.Status = BudgetStatus.Rejeitado;
        var updatedBudget = await _repo.UpdateBudgetAsync(churchId, budget);

        await _uow.Commit();

        result.Result = _mapper.Map<BudgetViewModel>(updatedBudget);

        return result;
    }

    public async Task<DomainNotificationsResult<BudgetViewModel>> ContestBudget(Guid guid)
    {
        var churchId = _userContext.GetChurchId();
        var result = new DomainNotificationsResult<BudgetViewModel>();

        var budget = await _repo.GetBudgetByGuidAsync(churchId, guid);

        if (budget == null)
            result.Notifications.Add("Orçamento não encontrado.");

        if (budget.Status == BudgetStatus.Contestado)
            result.Notifications.Add("Orçamento já está contestado.");

        if (result.Notifications.Any())
            return result;

        budget.Status = BudgetStatus.Contestado;
        var updatedBudget = await _repo.UpdateBudgetAsync(churchId, budget);

        await _uow.Commit();

        result.Result = _mapper.Map<BudgetViewModel>(updatedBudget);

        return result;
    }

    public async Task<DomainNotificationsResult<BudgetViewModel>> EndBudget(Guid guid)
    {
        var churchId = _userContext.GetChurchId();
        var result = new DomainNotificationsResult<BudgetViewModel>();

        var budget = await _repo.GetBudgetByGuidAsync(churchId, guid);

        if (budget == null)
            result.Notifications.Add("Orçamento não encontrado.");

        if (budget.Status == BudgetStatus.Encerrado)
            result.Notifications.Add("Orçamento já está encerrado.");

        if (result.Notifications.Any())
            return result;

        budget.Status = BudgetStatus.Encerrado;
        var updatedBudget = await _repo.UpdateBudgetAsync(churchId, budget);

        await _uow.Commit();

        result.Result = _mapper.Map<BudgetViewModel>(updatedBudget);

        return result;
    }

    public async Task<DomainNotificationsResult<int>> CountBudgets()
    {
        var result = new DomainNotificationsResult<int>();
        var churchId = _userContext.GetChurchId();

        var totalBudgets = await _repo.CountBudgets(churchId);

        result.Result = totalBudgets;

        return result;
    }
}
