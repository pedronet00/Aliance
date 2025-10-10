using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.DTOs;

public class CellMeetingDTO
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public string Theme { get; set; }

    public DateTime Date { get; set; }

    public Guid LocationGuid { get; set; }

    public Guid CellGuid { get; set; }

    public string LeaderGuid { get; set; }

}
