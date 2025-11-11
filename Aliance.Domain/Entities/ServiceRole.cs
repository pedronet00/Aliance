using Aliance.Domain.Enums;

namespace Aliance.Domain.Entities;

public class ServiceRole : BaseEntity
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public int ServiceId { get; set; }
    public Service Service { get; set; } = null!;
    public string MemberId { get; set; }
    public ApplicationUser Member { get; set; } = null!;
    public ServiceRoles Role { get; set; }


    private ServiceRole() { }

    public ServiceRole(int serviceId, string memberId, ServiceRoles role)
    {
        Guid = Guid.NewGuid();
        ServiceId = serviceId;
        MemberId = memberId;
        Role = role;
    }
}
