using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Services;

public class AccountPayableService : IAccountPayableService
{
    private readonly IAccountPayableRepository _repo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserContextService _userContext;
    private readonly IExpenseRepository _expenseRepository;

    public AccountPayableService(IAccountPayableRepository repo, IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContext, IExpenseRepository expenseRepository)
    {
        _repo = repo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userContext = userContext;
        _expenseRepository = expenseRepository;
    }

    public async Task<AccountPayableViewModel> AddAsync(AccountPayableDTO accountPayable)
    {
        if (accountPayable is null)
            throw new ArgumentNullException(nameof(accountPayable), "Account payable cannot be null.");

        var entity = _mapper.Map<AccountPayableDTO, AccountPayable>(accountPayable);

        var addedEntity = await _repo.AddAsync(entity);
        
        await _unitOfWork.Commit();
        
        return _mapper.Map<AccountPayable, AccountPayableViewModel>(addedEntity);
    }

    public async Task<bool> DeleteAsync(Guid guid)
    {
        var churchId = _userContext.GetChurchId();
        var accountPayable = await _repo.GetByGuidAsync(churchId, guid);
        
        if (accountPayable is null)
            return false;
        
        var result = await _repo.DeleteAsync(churchId, guid);
        
        if (result)
            await _unitOfWork.Commit();
        
        return result;
    }

    public async Task<IEnumerable<AccountPayableViewModel>> GetAllAsync()
    {
        var churchId = _userContext.GetChurchId();
        var accountPayables = await _repo.GetAllAsync(churchId);
        
        return _mapper.Map<IEnumerable<AccountPayable>, IEnumerable<AccountPayableViewModel>>(accountPayables);
    }

    public async Task<AccountPayableViewModel?> GetByGuidAsync(Guid guid)
    {
        var churchId = _userContext.GetChurchId();
        var accountPayable = await _repo.GetByGuidAsync(churchId, guid);
        
        if (accountPayable is null)
            return null;
        
        return _mapper.Map<AccountPayable, AccountPayableViewModel>(accountPayable);
    }

    public async Task<AccountPayableViewModel> ToggleStatus(Guid guid, AccountStatus status)
    {
        var churchId = _userContext.GetChurchId();
        var account = await _repo.GetByGuidAsync(churchId, guid);

        if (account == null)
            throw new Exception("Conta não encontrada.");

        switch (status)
        {
            case AccountStatus.Atrasada:
            case AccountStatus.Cancelada:
            case AccountStatus.Parcial:
                account.AccountStatus = status;
                break;

            case AccountStatus.Paga:
                account.AccountStatus = AccountStatus.Paga;

                if (account.PaymentDate is null)
                    account.PaymentDate = DateTime.Now;

                var expense = new Expense(
                    $"Pagamento da conta: {account.Description}",
                    account.Amount,
                    account.Id
                )
                {
                    Category = FinancialExpenseCategory.ContaPagar,
                    ChurchId = churchId
                };

                await _expenseRepository.InsertExpense(expense);
                break;
        }

        await _repo.UpdateAsync(churchId, account);
        await _unitOfWork.Commit();

        return _mapper.Map<AccountPayableViewModel>(account);
    }


    public async Task<AccountPayableViewModel> UpdateAsync(AccountPayableDTO accountPayable)
    {
        var churchId = _userContext.GetChurchId();
        if (accountPayable is null)
            throw new ArgumentNullException(nameof(accountPayable), "Account payable cannot be null.");
        
        var existingEntity = await _repo.GetByGuidAsync(churchId, accountPayable.Guid);
        
        if (existingEntity is null)
            throw new KeyNotFoundException($"Account payable with ID {accountPayable.Id} not found.");
        
        var entity = _mapper.Map<AccountPayableDTO, AccountPayable>(accountPayable, existingEntity);
        
        var updatedEntity = await _repo.UpdateAsync(churchId, entity);
        
        await _unitOfWork.Commit();
        
        return _mapper.Map<AccountPayable, AccountPayableViewModel>(updatedEntity);
    }
}
