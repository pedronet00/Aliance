using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;

namespace Aliance.Application.Interfaces;

public interface IMissionCampaignService
{
    Task<DomainNotificationsResult<PagedResult<MissionCampaignViewModel>>> GetAllAsync(int pageNumber, int pageSize);

    Task<DomainNotificationsResult<MissionCampaignViewModel>> GetByGuidAsync(Guid guid);

    Task<DomainNotificationsResult<MissionCampaignViewModel>> AddAsync(MissionCampaignDTO missionCampaign);

    Task<DomainNotificationsResult<MissionCampaignViewModel>> UpdateAsync(MissionCampaignDTO missionCampaign);

    Task<DomainNotificationsResult<bool>> DeleteAsync(Guid guid);
}
