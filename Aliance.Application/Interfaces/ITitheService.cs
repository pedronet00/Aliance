using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Notifications;

namespace Aliance.Application.Interfaces;

public interface ITitheService
{
    Task<DomainNotificationsResult<IEnumerable<TitheViewModel>>> GetTithes();

    Task<DomainNotificationsResult<TitheViewModel>> GetTitheByGuid(Guid guid);

    Task<DomainNotificationsResult<TitheViewModel>> InsertTithe(TitheDTO tithe);

    Task<DomainNotificationsResult<TitheViewModel>> UpdateTithe(TitheDTO tithe);

    Task<DomainNotificationsResult<TitheViewModel>> DeleteTithe(Guid guid);

    Task<DomainNotificationsResult<IEnumerable<TitheViewModel>>> GetTithesByUser(Guid userId);

    Task<DomainNotificationsResult<decimal>> GetTotalTithesByUser(Guid userId);

    Task<DomainNotificationsResult<decimal>> GetTotalTithes();
}
