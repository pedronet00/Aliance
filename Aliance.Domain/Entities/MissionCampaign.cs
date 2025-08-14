using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities;

public class MissionCampaign
{
    public int Id { get; set; }

    public string Name { get; set; }

    public Territorials Type { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal TargetAmount { get; set; }

    public decimal CollectedAmount { get; set; }

    public int ChurchId { get; set; }

    public Church Church { get; set; }
}
