using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.ViewModel;

public class WorshipTeamRehearsalViewModel
{
    public int Id { get; set; }
    public Guid Guid { get; private set; }
    public DateTime RehearsalDate { get; set; }
    public int WorshipTeamId { get; set; }

    public MeetingStatus Status { get; set; }
}
