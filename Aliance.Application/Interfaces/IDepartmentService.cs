using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;

namespace Aliance.Application.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentViewModel>> GetAllDepartments();

    Task<DepartmentViewModel> GetDepartmentById(int id);

    Task<DepartmentViewModel> InsertDepartment(DepartmentDTO department);

    Task<bool> UpdateDepartment(DepartmentDTO department);

    Task<bool> DeleteDepartment(int id);
}
