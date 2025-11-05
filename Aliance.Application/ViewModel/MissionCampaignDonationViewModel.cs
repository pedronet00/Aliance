using Aliance.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.ViewModel;

public class MissionCampaignDonationViewModel
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public string UserId { get; set; }

    public string UserName { get; set; }

    public int CampaignId { get; set; }

    public Money Amount { get; set; }
}
