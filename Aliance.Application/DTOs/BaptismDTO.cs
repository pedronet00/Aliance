using Aliance.Domain.Constants;
using Aliance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.DTOs;

public class BaptismDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    public string? PastorId { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    public string? UserId { get; set; }
}
