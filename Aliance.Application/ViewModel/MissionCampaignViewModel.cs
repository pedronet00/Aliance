using Aliance.Domain.Enums;

namespace Aliance.Application.ViewModel
{
    public class MissionCampaignViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Territorials Type { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal TargetAmount { get; set; }

        public decimal CollectedAmount { get; set; }

        public int ChurchId { get; set; }
    }
}
