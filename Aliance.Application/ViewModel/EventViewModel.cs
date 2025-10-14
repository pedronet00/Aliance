using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.ViewModel;

public class EventViewModel
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime Date { get; set; }

    public decimal Cost { get; set; }

    public int LocationId { get; set; }
    public int CostCenterId { get; set; }
    public string LocationName { get; set; }

    public MeetingStatus Status { get; set; }
}
