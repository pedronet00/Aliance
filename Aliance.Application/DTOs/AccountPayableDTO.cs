using Aliance.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using Aliance.Domain.Constants;

namespace Aliance.Application.DTOs;

public class AccountPayableDTO
{
    public int Id { get; set; }
    public Guid Guid { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    [StringLength(200, MinimumLength = 3, ErrorMessage = DataAnnotationMessages.STRLEN)]
    public string? Description { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    [Range(0.01, 999999.99, ErrorMessage = DataAnnotationMessages.DECIMAL_RANGE)]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    public DateTime DueDate { get; set; }

    public DateTime? PaymentDate { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    public AccountStatus AccountStatus { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    public int CostCenterId { get; set; }
}
