using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface IServiceRepository
{
    Task<IEnumerable<Service>> GetServices(int churchId);

    Task<Service> GetServiceByGuid(Guid serviceGuid, int churchId);

    Task<Service> AddService(Service service);

    Task<Service> UpdateService(Service service);

    Task<bool> DeleteService(Guid serviceGuid, int churchId);

    Task<Service> ToggleStatus(Guid serviceGuid, int churchId, ServiceStatus status);

    Task<bool> ServiceExists(int locationId, DateTime date, int churchId);

    Task<Service> GetServiceById(int churchId, int serviceId);
}
