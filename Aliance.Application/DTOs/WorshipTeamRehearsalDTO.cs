using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.DTOs;

public class WorshipTeamRehearsalDTO
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public DateTime RehearsalDate { get; set; }
    public Guid WorshipTeamId { get; set; }

    public MeetingStatus Status { get; set; }
}
