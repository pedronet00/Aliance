using Aliance.Domain.Entities;

namespace Aliance.Domain.Interfaces;

public interface IMissionCampaignRepository
{
    Task<IEnumerable<MissionCampaign>> GetAllAsync();

    Task<MissionCampaign?> GetByIdAsync(int id);

    Task<MissionCampaign> AddAsync(MissionCampaign missionCampaign);

    Task<MissionCampaign> UpdateAsync(MissionCampaign missionCampaign);

    Task<bool> DeleteAsync(int id);
}
