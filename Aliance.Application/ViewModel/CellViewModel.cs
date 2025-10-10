using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.ViewModel;

public class CellViewModel
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public string? Name { get; set; }

    public int LocationId { get; set; }

    public string? LeaderId { get; set; }

    public Weekdays MeetingDay { get; set; }

    public string? CellBanner { get; set; }

    public int ChurchId { get; set; }

    public string LocationName { get; set; }

    public string LeaderName { get; set; }
}
