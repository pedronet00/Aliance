using Aliance.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.DTOs;

public class MissionCampaignDonationDTO
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public Guid UserId { get; set; }

    public Guid CampaignGuid { get; set; }

    public decimal Amount { get; set; }

}
