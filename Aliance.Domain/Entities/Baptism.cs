using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities;

public class Baptism
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public string PastorId { get; set; }
    public ApplicationUser Pastor { get; set; } = null!;

    public string UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;

    public int ChurchId { get; set; }
}
