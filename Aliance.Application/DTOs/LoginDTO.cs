using Aliance.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace Aliance.Application.DTOs;

public class LoginDTO
{
    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    public string? Email { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    public string? Password { get; set; }
}
