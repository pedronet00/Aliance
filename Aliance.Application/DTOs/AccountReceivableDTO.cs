using Aliance.Domain.Constants;
using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.DTOs;

public class AccountReceivableDTO
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
