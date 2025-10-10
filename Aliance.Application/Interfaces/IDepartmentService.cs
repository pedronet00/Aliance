using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Notifications;

namespace Aliance.Application.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentViewModel>> GetAllDepartments();

    Task<DomainNotificationsResult<DepartmentViewModel>> GetDepartmentById(int id);

    Task<DomainNotificationsResult<DepartmentViewModel>> InsertDepartment(DepartmentDTO department);

    Task<DomainNotificationsResult<DepartmentViewModel>> UpdateDepartment(DepartmentDTO department);

    Task<DomainNotificationsResult<bool>> DeleteDepartment(int id);

    Task<DomainNotificationsResult<DepartmentViewModel>> ActivateDepartment(int id);

    Task<DomainNotificationsResult<DepartmentViewModel>> DeactivateDepartment(int id);
}
