using Aliance.Application.Interfaces;
using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Services;

public class RoutineService : IRoutineService
{
    private readonly IMapper _mapper;
    private readonly IUserContextService _userContext;
    private readonly IAccountPayableRepository _accountPayableRepo;
    private readonly IAccountReceivableRepository _accountReceivableRepo;
    private readonly IAutomaticAccountsRepository _automaticAccountsRepo;
    private readonly IUnitOfWork _uow;

    public RoutineService(
        IMapper mapper,
        IUserContextService userContext,
        IAccountPayableRepository accountPayableRepo,
        IAccountReceivableRepository accountReceivableRepo,
        IUnitOfWork uow)
    {
        _mapper = mapper;
        _userContext = userContext;
        _accountPayableRepo = accountPayableRepo;
        _accountReceivableRepo = accountReceivableRepo;
        _uow = uow;
    }

    public async Task<DomainNotificationsResult<string>> UpdateExpiringAccountsStatus()
    {
        var result = new DomainNotificationsResult<string>();
        var churchId = _userContext.GetChurchId();
        var totalPayable = 0;
        var totalReceivable = 0;
        var totalAccounts = 0;

        var accountsPayable = await _accountPayableRepo.GetExpiringAccounts();

        foreach(var ap in accountsPayable)
        {
            ap.AccountStatus = Domain.Enums.AccountStatus.Atrasada;

            await _accountPayableRepo.UpdateAsync(churchId, ap);

            totalPayable++;
            totalAccounts++;
        }

        var accountsReceivable = await _accountReceivableRepo.GetExpiringAccounts();

        foreach (var ap in accountsReceivable)
        {
            ap.AccountStatus = Domain.Enums.AccountStatus.Atrasada;

            await _accountReceivableRepo.UpdateAsync(churchId, ap);

            totalReceivable++;
            totalAccounts++;
        }

        await _uow.Commit();

        result.Result = $"Foram atualizadas {totalAccounts} contas, sendo {totalPayable} contas a pagar e {totalReceivable} a receber.";

        return result;
    }

    public async Task<DomainNotificationsResult<string>> InsertAutomaticAccounts()
    {
        var result = new DomainNotificationsResult<string>();
        //var churchId = _userContext.GetChurchId();

        var totalAccountsInserted = 0;
        var totalAccountsPayableInserted = 0;
        var totalAccountsReceivableInserted = 0; 

        var accounts = await _automaticAccountsRepo.GetAll();

        foreach (var account in accounts)
        {
            if (account.DueDay - 10 == DateTime.Today.Day)
            {
                var dueDate = DateTime.Today.AddDays(10);

                switch (account.AccountType)
                {
                    case Domain.Enums.AccountType.Payable:
                        var newAccountPayable = new AccountPayable($"Conta automática: {account.Description}", account.Amount, dueDate, account.CostCenterId);

                        await _accountPayableRepo.AddAsync(newAccountPayable);
                        totalAccountsInserted++;
                        totalAccountsPayableInserted++;
                        break;

                    case Domain.Enums.AccountType.Receivable:
                        var newAccountReceivable = new AccountReceivable($"Conta automática: {account.Description}", account.Amount, dueDate, account.CostCenterId);

                        await _accountReceivableRepo.AddAsync(newAccountReceivable);
                        totalAccountsInserted++;
                        totalAccountsReceivableInserted++;
                        break;
                }
            }
        }

        await _uow.Commit();

        result.Result = $"Foram geradas {totalAccountsInserted} contas com sucesso nessa rotina, sendo {totalAccountsPayableInserted} contas a pagar e {totalAccountsReceivableInserted} contas a receber.";

        return result;
    }
}
