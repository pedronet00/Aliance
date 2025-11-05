using Aliance.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities;

public class MissionCampaignDonation
{

    public int Id { get; set; }

    public Guid Guid { get; set; }

    public string UserId { get; set; }

    public ApplicationUser User { get; set; }

    public int CampaignId { get; set; }

    public MissionCampaign Campaign { get; set; }

    public Money Amount { get; set; }

    public MissionCampaignDonation() { }
    public MissionCampaignDonation(string userId, int campaignId, Money amount)
    {
        UserId = userId;
        CampaignId = campaignId;
        Amount = amount;
    }
}
