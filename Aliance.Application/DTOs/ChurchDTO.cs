using Aliance.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace Aliance.Application.DTOs;

public class ChurchDTO
{

    public int Id { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    public string Name { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    [StringLength(50, ErrorMessage = DataAnnotationMessages.STRLEN_MAX)]
    public string Email { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    [StringLength(20, MinimumLength = 10, ErrorMessage = DataAnnotationMessages.STRLEN)]
    public string Phone { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    [StringLength(100, ErrorMessage = DataAnnotationMessages.STRLEN_MAX)]
    public string Address { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    [StringLength(50, ErrorMessage = DataAnnotationMessages.STRLEN_MAX)]
    public string City { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    [StringLength(50, ErrorMessage = DataAnnotationMessages.STRLEN_MAX)]
    public string State { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    [StringLength(50, ErrorMessage = DataAnnotationMessages.STRLEN_MAX)]
    public string Country { get; set; }
}
