using Aliance.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using Aliance.Domain.Constants;

namespace Aliance.Application.DTOs;

public class AccountPayableDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    [StringLength(200, MinimumLength = 3, ErrorMessage = DataAnnotationMessages.STRLEN)]
    public string? Description { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    [StringLength(10, MinimumLength = 1, ErrorMessage = DataAnnotationMessages.STRLEN)]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    public DateTime DueDate { get; set; }

    public DateTime? PaymentDate { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    public AccountStatus AccountStatus { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    public int CostCenterId { get; set; }
}
