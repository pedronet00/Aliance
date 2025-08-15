using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Enums;

public enum AccountStatus
{
    Unpaid = 1,
    Paid = 2,
    Overdue = 3,
    PartiallyPaid = 4,
    Cancelled = 5,
    Pending = 6,
}
