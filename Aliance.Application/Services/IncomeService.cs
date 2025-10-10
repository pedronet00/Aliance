using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Application.ViewModels;
using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aliance.Application.Services;

public class IncomeService : IIncomeService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIncomeRepository _repo;
    private readonly IUserContextService _userContext;

    public IncomeService(IMapper mapper, IUnitOfWork unitOfWork, IIncomeRepository repo, IUserContextService userContext)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repo = repo;
        _userContext = userContext;
    }

    public async Task<DomainNotificationsResult<IncomeViewModel>> DeleteIncome(Guid guid)
    {
        var result = new DomainNotificationsResult<IncomeViewModel>();
        var churchId = _userContext.GetChurchId();

        var income = await _repo.GetIncomeByGuid(churchId, guid);
        if (income is null)
        {
            result.Notifications.Add("Entrada não existe.");
            return result;
        }

        await _repo.DeleteIncome(churchId, income);
        await _unitOfWork.Commit();

        result.Result = _mapper.Map<IncomeViewModel>(income);
        return result;
    }

    public async Task<IEnumerable<IncomeViewModel>> GetAllIncomes()
    {
        var churchId = _userContext.GetChurchId();
        var incomes = await _repo.GetAllIncomes(churchId);
        return _mapper.Map<IEnumerable<IncomeViewModel>>(incomes);
    }

    public async Task<DomainNotificationsResult<IncomeViewModel>> GetIncomeByGuid(Guid guid)
    {
        var result = new DomainNotificationsResult<IncomeViewModel>();
        var churchId = _userContext.GetChurchId();

        var income = await _repo.GetIncomeByGuid(churchId, guid);
        if (income is null)
        {
            result.Notifications.Add("Entrada não encontrada.");
            return result;
        }

        result.Result = _mapper.Map<IncomeViewModel>(income);
        return result;
    }

    public async Task<DomainNotificationsResult<IEnumerable<IncomeViewModel>>> GetIncomesByCategory(FinancialIncomingCategory category)
    {
        var result = new DomainNotificationsResult<IEnumerable<IncomeViewModel>>();
        var churchId = _userContext.GetChurchId();

        var incomes = await _repo.GetIncomesByCategory(churchId, category);
        if (!incomes.Any())
        {
            result.Notifications.Add("Nenhuma entrada encontrada para esta categoria.");
            return result;
        }

        result.Result = _mapper.Map<IEnumerable<IncomeViewModel>>(incomes);
        return result;
    }

    public async Task<DomainNotificationsResult<IEnumerable<IncomeMonthlyTotalViewModel>>> GetMonthlyTotals(int year)
    {
        var result = new DomainNotificationsResult<IEnumerable<IncomeMonthlyTotalViewModel>>();
        var churchId = _userContext.GetChurchId();

        var totals = await _repo.GetMonthlyTotals(churchId, year);
        if (!totals.Any())
        {
            result.Notifications.Add("Nenhum registro de entrada encontrado para este ano.");
            return result;
        }

        result.Result = totals
            .Select(t => new IncomeMonthlyTotalViewModel
            {
                Month = int.TryParse(t.Key, out var month) ? month : 0, // conversão segura
                Total = t.Value
            })
            .ToList();

        return result;
    }



    public async Task<DomainNotificationsResult<IncomeRangeTotalViewModel>> GetTotalByDateRange(DateTime start, DateTime end)
    {
        var result = new DomainNotificationsResult<IncomeRangeTotalViewModel>();
        var churchId = _userContext.GetChurchId();

        if (start > end)
        {
            result.Notifications.Add("Data inicial não pode ser maior que a data final.");
            return result;
        }

        var total = await _repo.GetTotalByDateRange(churchId, start, end);
        result.Result = _mapper.Map<IncomeRangeTotalViewModel>(total);

        return result;
    }

    public async Task<DomainNotificationsResult<IncomeViewModel>> InsertIncome(IncomeDTO incomeDto)
    {
        var result = new DomainNotificationsResult<IncomeViewModel>();
        var churchId = _userContext.GetChurchId();

        var entity = _mapper.Map<Income>(incomeDto);

        await _repo.InsertIncome(entity);
        await _unitOfWork.Commit();

        result.Result = _mapper.Map<IncomeViewModel>(entity);
        return result;
    }

    public async Task<DomainNotificationsResult<IncomeViewModel>> UpdateIncome(IncomeDTO incomeDto)
    {
        var result = new DomainNotificationsResult<IncomeViewModel>();
        var churchId = _userContext.GetChurchId();

        var existing = await _repo.GetIncomeByGuid(churchId, incomeDto.Guid);
        if (existing is null)
        {
            result.Notifications.Add("Entrada não encontrada para atualização.");
            return result;
        }

        _mapper.Map(incomeDto, existing);

        await _repo.UpdateIncome(churchId, existing);
        await _unitOfWork.Commit();

        result.Result = _mapper.Map<IncomeViewModel>(existing);
        return result;
    }
}
