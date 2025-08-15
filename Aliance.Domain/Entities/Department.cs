using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities;

public class Department
{

    public int Id { get; set; }

    public string Name { get; set; }

    public bool Status { get; set; }

    public int ChurchId { get; set; }

    public Church Church { get; set; }

    public ICollection<CostCenter> CostCenter { get; set; } = new List<CostCenter>();
}
