using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.DTOs;

public class ServiceDTO
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public DateTime Date { get; set; }

    public ServiceStatus Status { get; set; }

    public int LocationId{ get; set; }

    public int ChurchId { get; set; }
}
