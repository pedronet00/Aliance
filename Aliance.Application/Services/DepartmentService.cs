using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using AutoMapper;

namespace Aliance.Application.Services;

public class DepartmentService : IDepartmentService
{

    private readonly IDepartmentRepository _repo;
    private readonly IMapper _mapper;

    public DepartmentService(IDepartmentRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<bool> DeleteDepartment(int id)
    {
        var department = await _repo.GetDepartmentById(id);

        if (department is null)
            throw new ArgumentNullException(nameof(department), "Department not found");

        await _repo.DeleteDepartment(id);

        return true;
    }

    public async Task<IEnumerable<DepartmentViewModel>> GetAllDepartments()
    {
        var departments = await _repo.GetAllDepartments();

        var departmentVMs = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);

        return departmentVMs;
    }

    public async Task<DepartmentViewModel> GetDepartmentById(int id)
    {
        var department = await _repo.GetDepartmentById(id);

        if (department is null)
            throw new ArgumentNullException(nameof(department), "Department not found");

        var departmentVM = _mapper.Map<DepartmentViewModel>(department);

        return departmentVM;
    }

    public async Task<DepartmentViewModel> InsertDepartment(DepartmentDTO departmentDTO)
    {
        if (departmentDTO is null)
            throw new ArgumentNullException(nameof(departmentDTO));

        var department = _mapper.Map<Department>(departmentDTO);

        await _repo.InsertDepartment(department);

        var newDepartmentVM = _mapper.Map<DepartmentViewModel>(department);

        return newDepartmentVM;
    }


    public async Task<bool> UpdateDepartment(DepartmentDTO departmentDTO)
    {
        if(departmentDTO is null)
            throw new ArgumentNullException(nameof(departmentDTO));

        var department = _mapper.Map<Department>(departmentDTO);

        await _repo.UpdateDepartment(department);

        return true;
    }
}
