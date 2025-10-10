using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces
{
    public interface IMissionCampaignService
    {
        Task<IEnumerable<MissionCampaignViewModel>> GetAllAsync();

        Task<MissionCampaignViewModel> GetByIdAsync(int id);

        Task<MissionCampaignViewModel> AddAsync(MissionCampaignDTO missionCampaign);

        Task<MissionCampaignViewModel> UpdateAsync(MissionCampaignDTO missionCampaign);

        Task<bool> DeleteAsync(int id);
    }
}
