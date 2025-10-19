using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Enums;

public enum PaymentStatus
{
    Active = 1,
    Pending = 2,
    Expired = 3,
    Canceled = 4
}
