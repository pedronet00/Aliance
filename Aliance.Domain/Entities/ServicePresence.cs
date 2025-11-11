using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities;

public class ServicePresence : BaseEntity
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public Guid ServiceGuid { get; set; }
    public Service Service { get; set; }

    public Guid UserId { get; set; }

    public ApplicationUser User { get; set; }

    private ServicePresence() { }
    public ServicePresence(Guid serviceGuid, Guid userId)
    {
        Guid = Guid.NewGuid();
        ServiceGuid = serviceGuid;
        UserId = userId;
    }
}
