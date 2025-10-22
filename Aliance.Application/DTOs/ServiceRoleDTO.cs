using Aliance.Domain.Enums;

namespace Aliance.Application.DTOs;

public class ServiceRoleDTO
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public Guid ServiceGuid { get; set; }
    public Guid MemberId { get; set; }
    public ServiceRoles Role { get; set; }
}
