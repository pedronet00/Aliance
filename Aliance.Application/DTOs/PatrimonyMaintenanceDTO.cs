using Aliance.Domain.Constants;
using Aliance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.DTOs;

public class PatrimonyMaintenanceDTO
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public DateTime MaintenanceDate { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    public string Description { get; set; }
    public int PatrimonyId { get; set; }
    public string? PatrimonyName { get; set; }
}
