using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
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

public class AutomaticAccountsService : IAutomaticAccountsService
{
    private readonly IAutomaticAccountsRepository _repo;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IUserContextService _userContext;
    private readonly IAccountPayableRepository _accountPayableRepository;
    private readonly IAccountReceivableRepository _accountReceivableRepository;

    public AutomaticAccountsService(IAutomaticAccountsRepository repo, IUnitOfWork uow, IMapper mapper, IUserContextService userContext, IAccountPayableRepository accountPayableRepository, IAccountReceivableRepository accountReceivableRepository)
    {
        _repo = repo;
        _uow = uow;
        _mapper = mapper;
        _userContext = userContext;
        _accountPayableRepository = accountPayableRepository;
        _accountReceivableRepository = accountReceivableRepository;
    }

    public async Task<DomainNotificationsResult<AutomaticAccountsViewModel>> Delete(Guid guid)
    {
        var result = new DomainNotificationsResult<AutomaticAccountsViewModel>();

        var account = await _repo.GetByGuid(guid);

        if(account == null)
        {
            result.Notifications.Add("Conta automática não encontrada.");
            return result;
        }

        await _repo.Delete(account.Id);

        await _uow.Commit();

        result.Result = _mapper.Map<AutomaticAccountsViewModel>(account);

        return result;
    }

    public async Task<DomainNotificationsResult<PagedResult<AutomaticAccountsViewModel>>> GetAll(int pageNumber, int pageSize)
    {
        var result = new DomainNotificationsResult<PagedResult<AutomaticAccountsViewModel>>();
        var churchId = _userContext.GetChurchId();
        var accounts = await _repo.GetAllPaged(churchId, pageNumber, pageSize);

        var accountsVMs = _mapper.Map<IEnumerable<AutomaticAccounts>, IEnumerable<AutomaticAccountsViewModel>>(accounts.Items);

        result.Result = new PagedResult<AutomaticAccountsViewModel>(
            accountsVMs,
            accounts.TotalCount,
            accounts.CurrentPage,
            accounts.PageSize
        );

        return result;
    }

    public async Task<DomainNotificationsResult<AutomaticAccountsViewModel>> GetByGuid(Guid guid)
    {
        var result = new DomainNotificationsResult<AutomaticAccountsViewModel>();

        var account = await _repo.GetByGuid(guid);

        if(account == null)
        {
            result.Notifications.Add("Conta automática não encontrada.");
            return result;
        }

        result.Result = _mapper.Map<AutomaticAccountsViewModel>(account);

        return result;
    }

    public async Task<DomainNotificationsResult<AutomaticAccountsViewModel>> GetById(int id)
    {
        var result = new DomainNotificationsResult<AutomaticAccountsViewModel>();

        var account = await _repo.GetById(id);

        if (account == null)
        {
            result.Notifications.Add("Conta automática não encontrada.");
            return result;
        }

        result.Result = _mapper.Map<AutomaticAccountsViewModel>(account);

        return result;
    }

    public async Task<DomainNotificationsResult<AutomaticAccountsViewModel>> Insert(AutomaticAccountsDTO automaticAccount)
    {
        var result = new DomainNotificationsResult<AutomaticAccountsViewModel>();

        var entity = _mapper.Map<AutomaticAccounts>(automaticAccount);

        await _repo.Insert(entity);

        await _uow.Commit();

        result.Result = _mapper.Map<AutomaticAccountsViewModel>(entity);

        return result;
    }

    public async Task<DomainNotificationsResult<AutomaticAccountsViewModel>> Update(AutomaticAccountsDTO automaticAccount)
    {
        var result = new DomainNotificationsResult<AutomaticAccountsViewModel>();

        var entity = await _repo.GetByGuid(automaticAccount.Guid);

        if (entity == null)
        {
            result.Notifications.Add("Conta automática não encontrada.");
            return result;
        }

        _mapper.Map(automaticAccount, entity);

        _repo.Update(entity);
        await _uow.Commit();

        result.Result = _mapper.Map<AutomaticAccountsViewModel>(entity);
        return result;
    }

}
