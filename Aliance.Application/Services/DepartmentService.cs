using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using AutoMapper;

namespace Aliance.Application.Services;

public class DepartmentService : IDepartmentService
{

    private readonly IDepartmentRepository _repo;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;
    private readonly IUserContextService _context;

    public DepartmentService(IDepartmentRepository repo, IMapper mapper, IUnitOfWork uow, IUserContextService context)
    {
        _repo = repo;
        _mapper = mapper;
        _uow = uow;
        _context = context;
    }

    public async Task<DomainNotificationsResult<DepartmentViewModel>> ActivateDepartment(int id)
    {
        var result = new DomainNotificationsResult<DepartmentViewModel>();
        var churchId = _context.GetChurchId();

        var department = await _repo.GetDepartmentById(churchId, id);

        if (department is null)
            result.Notifications.Add("Departmento não encontrado.");

        if(result.Notifications.Any())
            return result;

        await _repo.ActivateDepartment(churchId, id);

        await _uow.Commit();

        result.Result = _mapper.Map<DepartmentViewModel>(department);

        return result;
    }

    public async Task<DomainNotificationsResult<DepartmentViewModel>> DeactivateDepartment(int id)
    {
        var result = new DomainNotificationsResult<DepartmentViewModel>();
        var churchId = _context.GetChurchId();

        var department = await _repo.GetDepartmentById(churchId, id);

        if (department is null)
            result.Notifications.Add("Departmento não encontrado.");

        if (result.Notifications.Any())
            return result;

        await _repo.DeactivateDepartment(churchId, id);

        await _uow.Commit();

        result.Result = _mapper.Map<DepartmentViewModel>(department);

        return result;
    }

    public async Task<DomainNotificationsResult<bool>> DeleteDepartment(int id)
    {
        var result = new DomainNotificationsResult<bool>();
        var churchId = _context.GetChurchId();

        var department = await _repo.GetDepartmentById(churchId, id);

        if (department is null)
            result.Notifications.Add("Departmento não encontrado.");

        if(result.Notifications.Any())
            return result;
        

        await _repo.DeleteDepartment(churchId, id);

        await _uow.Commit();

        result.Result = true;

        return result;
    }

    public async Task<IEnumerable<DepartmentViewModel>> GetAllDepartments()
    {
        var churchId = _context.GetChurchId();

        var departments = await _repo.GetAllDepartments(churchId);

        var departmentVMs = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);

        return departmentVMs;
    }

    public async Task<DomainNotificationsResult<DepartmentViewModel>> GetDepartmentById(int id)
    {
        var result = new DomainNotificationsResult<DepartmentViewModel>();
        var churchId = _context.GetChurchId();
        var department = await _repo.GetDepartmentById(churchId, id);

        if (department is null)
            result.Notifications.Add("Departmento não encontrado.");

        if (result.Notifications.Any())
            return result;

        result.Result = _mapper.Map<DepartmentViewModel>(department);

        return result;
    }

    public async Task<DomainNotificationsResult<DepartmentViewModel>> InsertDepartment(DepartmentDTO departmentDTO)
    {
        var result = new DomainNotificationsResult<DepartmentViewModel>();

        if (departmentDTO is null)
            result.Notifications.Add("Dados inválidos.");

        if (result.Notifications.Any())
            return result;

        var department = _mapper.Map<Department>(departmentDTO);

        await _repo.InsertDepartment(department);

        await _uow.Commit();

        result.Result = _mapper.Map<DepartmentViewModel>(department);

        return result;
    }


    public async Task<DomainNotificationsResult<DepartmentViewModel>> UpdateDepartment(DepartmentDTO departmentDTO)
    {
        var result = new DomainNotificationsResult<DepartmentViewModel>();
        var churchId = _context.GetChurchId();

        if (departmentDTO is null)
            result.Notifications.Add("Dados inválidos.");

        if(result.Notifications.Any())
            return result;

        var department = _mapper.Map<Department>(departmentDTO);

        await _repo.UpdateDepartment(churchId, department);

        await _uow.Commit();

        result.Result = _mapper.Map<DepartmentViewModel>(department);

        return result;
    }
}
