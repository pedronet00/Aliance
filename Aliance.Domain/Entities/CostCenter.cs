using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities;

public class CostCenter
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int DepartmentId { get; set; }

    public Department Department { get; set; }

    public int ChurchId { get; set; }

    public Church Church { get; set; }

    // Collections
    public ICollection<AccountPayable> AccountPayable { get; set; }
}
