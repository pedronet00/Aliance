using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.ViewModel;

public class PatrimonyMaintenanceViewModel
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public PatrimonyMaintenanceStatus Status { get; set; }
    public string? Description { get; set; }
    public int PatrimonyId { get; set; }
    public string? PatrimonyName { get; set; }
}
