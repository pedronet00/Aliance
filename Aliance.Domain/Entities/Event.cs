using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities;

public class Event
{

    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime Date { get; set; }

    public decimal Cost { get; set; }

    public int LocationId { get; set; }

    public Location? Location { get; set; }

    public int ClientId { get; set; }
}
