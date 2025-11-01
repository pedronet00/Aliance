using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Notifications;

namespace Aliance.Application.Interfaces;

public interface IWorshipTeamRehearsalService
{
    Task<DomainNotificationsResult<IEnumerable<WorshipTeamRehearsalViewModel>>> GetWorshipTeamRehearsals(Guid teamGuid);

    Task<DomainNotificationsResult<WorshipTeamRehearsalViewModel>> GetWorshipTeamRehearsalByGuid(Guid guid);

    Task<DomainNotificationsResult<WorshipTeamRehearsalViewModel>> InsertWorshipTeamRehearsal(WorshipTeamRehearsalDTO rehearsal);

    Task<DomainNotificationsResult<WorshipTeamRehearsalViewModel>> UpdateWorshipTeamRehearsal(WorshipTeamRehearsalDTO rehearsal);

    Task<DomainNotificationsResult<WorshipTeamRehearsalViewModel>> DeleteWorshipTeamRehearsal(Guid guid);

    Task<DomainNotificationsResult<WorshipTeamRehearsalViewModel>> GetNextWorshipTeamRehearsal(Guid teamGuid);
}
