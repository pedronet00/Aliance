using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Notifications;

public class DomainNotifications : DomainNotificationsBase
{
    public DomainNotifications() { }

    public DomainNotifications(string notification)
    {
        Add(notification);
    }
}
