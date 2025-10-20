using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities;

public class Service
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public DateTime Date { get; set; }

    public ServiceStatus Status { get; set; }

    public int LocationId { get; set; }
    public Location Location { get; set; }

    public int ChurchId { get; set; }
    public Church Church { get; set; }

    private Service() { }

    public Service(DateTime date, ServiceStatus status, int locationId, int churchId)
    {
        Guid = Guid.NewGuid();
        Date = date;
        Status = status;
        LocationId = locationId;
        ChurchId = churchId;
    }
}
