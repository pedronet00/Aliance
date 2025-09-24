using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Notifications;

public class DomainNotificationsResult<T> : DomainNotificationsBase
{
    public T Result { get; set; }

    public override bool HasResult => Result != null;

    public DomainNotificationsResult() { }

    public DomainNotificationsResult(string notification)
    {
        Add(notification);
    }
}
