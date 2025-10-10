using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.ViewModel;

public class LocationViewModel
{
    public int Id { get; set; }

    public Guid Guid { get; private set; }

    public string? Name { get; set; }

    public bool Status { get; set; }

    public int ChurchId { get; set; }
}
