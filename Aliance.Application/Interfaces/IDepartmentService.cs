using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;

namespace Aliance.Application.Interfaces;

public interface IDepartmentService
{
    Task<PagedResult<DepartmentViewModel>> GetDepartmentsPaged(int pageNumber, int pageSize);

    Task<DomainNotificationsResult<DepartmentViewModel>> GetDepartmentById(int id);

    Task<DomainNotificationsResult<DepartmentViewModel>> InsertDepartment(DepartmentDTO department);

    Task<DomainNotificationsResult<DepartmentViewModel>> UpdateDepartment(DepartmentDTO department);

    Task<DomainNotificationsResult<bool>> DeleteDepartment(int id);

    Task<DomainNotificationsResult<DepartmentViewModel>> ActivateDepartment(int id);

    Task<DomainNotificationsResult<DepartmentViewModel>> DeactivateDepartment(int id);
}
