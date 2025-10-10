using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.ViewModel;

public class CellMeetingViewModel
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public string Theme { get; set; }

    public DateTime Date { get; set; }

    public int LocationId { get; set; }
    public string LocationName { get; set; }

    public int CellId { get; set; }

    public string LeaderId { get; set; }
    public string LeaderName { get; set; }

    public MeetingStatus Status { get; set; }
}
