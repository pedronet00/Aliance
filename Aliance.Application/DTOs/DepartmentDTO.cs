using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aliance.Domain.Constants;

namespace Aliance.Application.DTOs;

public class DepartmentDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    public string? Name { get; set; }

    public bool? Status { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    public int? ChurchId { get; set; }
}
