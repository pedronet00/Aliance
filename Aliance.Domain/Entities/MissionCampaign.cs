using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities;

public class MissionCampaign : BaseEntity
{
    public int Id { get; set; }

    public Guid Guid { get; private set; }

    public string Name { get; set; }

    public Territorials Type { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal TargetAmount { get; set; }

    public decimal CollectedAmount { get; set; }

    public int ChurchId { get; set; }

    public Church Church { get; set; }

    #region Collections

    public ICollection<MissionCampaignDonation> Donations { get; set; } = new List<MissionCampaignDonation>();

    #endregion
    
    private MissionCampaign() { }

    public MissionCampaign(int id, Guid guid, string name, Territorials type, DateTime startDate, DateTime endDate, decimal targetAmount, decimal collectedAmount, int churchId)
    {
        Id = id;
        Guid = Guid.NewGuid();
        Name = name;
        Type = type;
        StartDate = startDate;
        EndDate = endDate;
        TargetAmount = targetAmount;
        CollectedAmount = collectedAmount;
        ChurchId = churchId;
    }

}
