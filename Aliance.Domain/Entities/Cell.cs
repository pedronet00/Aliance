using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities;

public class Cell
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public string Name { get; set; }

    public int LocationId { get; set; }

    public Location Location { get; set; }

    public string LeaderId { get; set; }

    public ApplicationUser Leader { get; set; }

    public Weekdays MeetingDay { get; set; }

    public string CellBanner { get; set; }

    public int ChurchId { get; set; }

    public Church Church { get; set; }
}
