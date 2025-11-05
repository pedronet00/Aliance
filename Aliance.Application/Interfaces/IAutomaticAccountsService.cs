using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface IAutomaticAccountsService
{
    Task<DomainNotificationsResult<PagedResult<AutomaticAccountsViewModel>>> GetAll(int pageNumber, int pageSize);

    Task<DomainNotificationsResult<AutomaticAccountsViewModel>> Insert(AutomaticAccountsDTO automaticAccount);

    Task<DomainNotificationsResult<AutomaticAccountsViewModel>> Update(AutomaticAccountsDTO automaticAccount);

    Task<DomainNotificationsResult<AutomaticAccountsViewModel>> Delete(Guid guid);

    Task<DomainNotificationsResult<AutomaticAccountsViewModel>> GetById(int id);

    Task<DomainNotificationsResult<AutomaticAccountsViewModel>> GetByGuid(Guid guid);

    Task<DomainNotificationsResult<string>> Routine();
}
