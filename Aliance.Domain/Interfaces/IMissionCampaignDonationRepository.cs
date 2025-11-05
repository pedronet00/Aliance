using Aliance.Domain.Entities;
using Aliance.Domain.Pagination;
using Aliance.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface IMissionCampaignDonationRepository
{
    Task<MissionCampaignDonation> GetByGuid(Guid guid);

    Task<PagedResult<MissionCampaignDonation>> ListByCampaign(int churchId, int campaignId, int pageNumber, int pageSize);

    Task<IEnumerable<MissionCampaignDonation>> ListByUser(int churchId, string userId);

    Task<MissionCampaignDonation> AddDonation(MissionCampaignDonation donation);

    Task<MissionCampaignDonation> DeleteDonation(int donationId);
}
