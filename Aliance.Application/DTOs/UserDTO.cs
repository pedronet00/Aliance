using Aliance.Domain.Constants;
using Aliance.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Aliance.Application.DTOs;

public class UserDTO
{
    public string? Id { get; set; }
    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    public string? UserName { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    public string? Email { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    public string? Password { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    public string? Role { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    public string? Phone { get; set; }

    public bool Status { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    public int ChurchId { get; set; }

}
