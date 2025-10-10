using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.DTOs;

public class PatrimonyMaintenanceDocumentDTO
{
    public Guid MaintenanceGuid { get; set; }

    [FromForm]
    public IFormFile File { get; set; } = null!;
}
