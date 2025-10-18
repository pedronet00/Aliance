using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities.Auth;

public class AsaasWebhookEvent
{
    public int Id { get; set; }
    public string Event { get; set; }
    public string Data { get; set; }
    public DateTime ReceivedAt { get; set; }
}
