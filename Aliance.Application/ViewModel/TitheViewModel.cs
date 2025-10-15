using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.ViewModel;

public class TitheViewModel
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public string UserId { get; set; }

    public string UserName { get; set; }
}
