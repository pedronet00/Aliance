using Aliance.Domain.Enums;
using Aliance.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace Aliance.Application.DTOs
{
    public class MissionCampaignDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
        [StringLength(100, MinimumLength = 3, ErrorMessage = DataAnnotationMessages.STRLEN)]
        public string? Name { get; set; }

        [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
        public Territorials Type { get; set; }

        [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
        public decimal TargetAmount { get; set; }

        [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
        public decimal CollectedAmount { get; set; }

        [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
        public int ChurchId { get; set; }

    }
}
