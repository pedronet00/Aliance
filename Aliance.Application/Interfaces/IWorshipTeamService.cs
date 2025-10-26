using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;

namespace Aliance.Application.Interfaces;

public interface IWorshipTeamService
{
    Task<PagedResult<WorshipTeamViewModel>> GetWorshipTeamsPaged(int pageNumber, int pageSize);

    Task<DomainNotificationsResult<WorshipTeamViewModel>> GetWorshipTeamByGuid(Guid guid);

    Task<DomainNotificationsResult<WorshipTeamViewModel>> InsertWorshipTeam(WorshipTeamDTO WorshipTeam);

    Task<DomainNotificationsResult<WorshipTeamViewModel>> UpdateWorshipTeam(WorshipTeamDTO WorshipTeam);

    Task<DomainNotificationsResult<bool>> DeleteWorshipTeam(Guid guid);

    Task<DomainNotificationsResult<WorshipTeamViewModel>> ActivateWorshipTeam(Guid guid);

    Task<DomainNotificationsResult<WorshipTeamViewModel>> DeactivateWorshipTeam(Guid guid);
}
