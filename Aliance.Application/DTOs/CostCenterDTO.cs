using Aliance.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using Aliance.Domain.Constants;

namespace Aliance.Application.DTOs;

public class CostCenterDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    [StringLength(100, MinimumLength = 3, ErrorMessage = DataAnnotationMessages.STRLEN)]
    public string? Name { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    public int DepartmentId { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    public int ChurchId { get; set; }

}
