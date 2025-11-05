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

public interface IMissionCampaignDonationService
{
    Task<DomainNotificationsResult<PagedResult<MissionCampaignDonationViewModel>>> ListByCampaign(Guid campaignGuid, int pageNumber, int pageSize);
    Task<DomainNotificationsResult<IEnumerable<MissionCampaignDonationViewModel>>> ListByUser(string userId);

    Task<DomainNotificationsResult<MissionCampaignDonationViewModel>> AddDonation(MissionCampaignDonationDTO donation);

    Task<DomainNotificationsResult<MissionCampaignDonationViewModel>> DeleteDonation(Guid donationGuid);
}
