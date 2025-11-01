using Aliance.Domain.Entities;
using Aliance.Domain.Pagination;

namespace Aliance.Domain.Interfaces;

public interface IMissionCampaignRepository
{
    Task<PagedResult<MissionCampaign>> GetAllAsync(int churchId, int pageNumber, int pageSize);

    Task<MissionCampaign?> GetByGuidAsync(int churchId, Guid guid);

    Task<MissionCampaign> AddAsync(MissionCampaign missionCampaign);

    Task<MissionCampaign> UpdateAsync(MissionCampaign missionCampaign);

    Task<bool> DeleteAsync(Guid guid);
}
