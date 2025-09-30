using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.DTOs;

public class PatrimonyDTO
{
    public int Id { get; set; }

    public Guid Guid { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal UnitValue { get; set; }
    public int Quantity { get; set; }
    public decimal TotalValue { get; set; }
    public DateTime AcquisitionDate { get; set; }
    public PatrimonyCondition Condition { get; set; }
    public int ChurchId { get; set; }
}
