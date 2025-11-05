using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Pagination;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Services;

public class AccountReceivableService : IAccountReceivableService
{
    private readonly IAccountReceivableRepository _repo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserContextService _userContext;
    private readonly IIncomeRepository _incomeRepository;

    public AccountReceivableService(IAccountReceivableRepository repo, IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContext, IIncomeRepository incomeRepository)
    {
        _repo = repo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userContext = userContext;
        _incomeRepository = incomeRepository;
    }

    public async Task<AccountReceivableViewModel> AddAsync(AccountReceivableDTO accountReceivable)
    {
        if (accountReceivable is null)
            throw new ArgumentNullException(nameof(accountReceivable), "Conta a receber não pode ser nula.");

        var entity = _mapper.Map<AccountReceivableDTO, AccountReceivable>(accountReceivable);

        var addedEntity = await _repo.AddAsync(entity);

        await _unitOfWork.Commit();

        return _mapper.Map<AccountReceivable, AccountReceivableViewModel>(addedEntity);
    }

    public async Task<bool> DeleteAsync(Guid guid)
    {
        var churchId = _userContext.GetChurchId();
        var accountReceivable = await _repo.GetByGuidAsync(churchId, guid);

        if (accountReceivable is null)
            return false;

        var result = await _repo.DeleteAsync(churchId, guid);

        if (result)
            await _unitOfWork.Commit();

        return result;
    }

    public async Task<PagedResult<AccountReceivableViewModel>> GetAllAsync(int pageNumber, int pageSize)
    {
        var churchId = _userContext.GetChurchId();
        var accountReceivables = await _repo.GetAllAsync(churchId, pageNumber, pageSize);

        var accountsVMs = _mapper.Map<IEnumerable<AccountReceivable>, IEnumerable<AccountReceivableViewModel>>(accountReceivables.Items);
        
        return new PagedResult<AccountReceivableViewModel>(
            accountsVMs,
            accountReceivables.TotalCount,
            accountReceivables.CurrentPage,
            accountReceivables.PageSize
        );
    }

    public async Task<AccountReceivableViewModel?> GetByGuidAsync(Guid guid)
    {
        var churchId = _userContext.GetChurchId();
        var accountReceivable = await _repo.GetByGuidAsync(churchId, guid);

        if (accountReceivable is null)
            return null;

        return _mapper.Map<AccountReceivable, AccountReceivableViewModel>(accountReceivable);
    }

    public async Task<AccountReceivableViewModel> ToggleStatus(Guid guid, AccountStatus status)
    {
        var churchId = _userContext.GetChurchId();
        var account = await _repo.GetByGuidAsync(churchId, guid);

        switch (status)
        {
            case AccountStatus.Atrasada:
                await _repo.ToggleStatus(churchId, guid, status);
                break;

            case AccountStatus.Paga:
                await _repo.ToggleStatus(churchId, guid, status);

                if (account.PaymentDate is null)
                    account.PaymentDate = DateTime.Now;

                var income = new Income(
                    $"Recebimento da conta: {account.Description}",
                    account.Amount,
                    account.Id
                );

                income.Category = FinancialIncomingCategory.ContaReceber;
                income.ChurchId = churchId;

                await _incomeRepository.InsertIncome(income);

                break;

            case AccountStatus.Cancelada:
                await _repo.ToggleStatus(churchId, guid, status);
                break;

            case AccountStatus.Parcial:
                await _repo.ToggleStatus(churchId, guid, status);
                break;
        }

        await _repo.UpdateAsync(churchId, account);
        await _unitOfWork.Commit();

        return _mapper.Map<AccountReceivableViewModel>(account);
    }

    public async Task<AccountReceivableViewModel> UpdateAsync(AccountReceivableDTO accountReceivable)
    {
        var churchId = _userContext.GetChurchId();
        if (accountReceivable is null)
            throw new ArgumentNullException(nameof(accountReceivable), "Conta a receber não pode ser nula.");

        var existingEntity = await _repo.GetByGuidAsync(churchId, accountReceivable.Guid);

        if (existingEntity is null)
            throw new KeyNotFoundException($"Conta a receber não encontrada.");

        var entity = _mapper.Map<AccountReceivableDTO, AccountReceivable>(accountReceivable, existingEntity);

        var updatedEntity = await _repo.UpdateAsync(churchId, entity);

        await _unitOfWork.Commit();

        return _mapper.Map<AccountReceivable, AccountReceivableViewModel>(updatedEntity);
    }
}
