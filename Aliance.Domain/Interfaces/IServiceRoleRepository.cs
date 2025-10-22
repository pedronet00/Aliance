using Aliance.Domain.Entities;

namespace Aliance.Domain.Interfaces;

public interface IServiceRoleRepository
{
    Task<IEnumerable<ServiceRole>> List(int serviceId);
    Task<ServiceRole> Add(ServiceRole serviceRole);
    Task<bool> Delete(Guid serviceRoleGuid, int churchId);
}
