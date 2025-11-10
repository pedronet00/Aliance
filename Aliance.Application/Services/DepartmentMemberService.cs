using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Services;

public class DepartmentMemberService : IDepartmentMemberService
{
    private readonly IDepartmentMemberRepository _repo;
    private readonly IDepartmentRepository _departmentRepo;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IUserContextService _userContext;

    public DepartmentMemberService(IDepartmentMemberRepository repo, IDepartmentRepository departmentRepo, IUnitOfWork uow, IMapper mapper, IUserContextService userContext)
    {
        _repo = repo;
        _departmentRepo = departmentRepo;
        _uow = uow;
        _mapper = mapper;
        _userContext = userContext;
    }

    public async Task<DomainNotificationsResult<DepartmentMemberViewModel>> DeleteDepartmentMember(Guid departmentGuid, string memberId)
    {
        var result = new DomainNotificationsResult<DepartmentMemberViewModel>();
        var churchId = _userContext.GetChurchId();
        var department = await _departmentRepo.GetDepartmentByGuid(churchId, departmentGuid);

        if (department == null) 
        {
            result.Notifications.Add("Departamento não existe.");
            return result;
        }

        var member = await _repo.DeleteDepartmentMember(department.Id, memberId);

        await _uow.Commit();

        result.Result = _mapper.Map<DepartmentMemberViewModel>(member);

        return result;
    }

    public async Task<DomainNotificationsResult<PagedResult<DepartmentMemberViewModel>>> GetDepartmentMembers(Guid departmentGuid, int pageNumber, int pageSize)
    {
        var result = new DomainNotificationsResult<PagedResult<DepartmentMemberViewModel>>();
        var churchId = _userContext.GetChurchId();
        var department = await _departmentRepo.GetDepartmentByGuid(churchId, departmentGuid);

        if (department == null)
        {
            result.Notifications.Add("Departamento não existe.");
            return result;
        }

        var departmentMembers = await _repo.GetDepartmentMembers(churchId, department.Id, pageNumber, pageSize);

        var departmentMembersViewModel = _mapper.Map<IEnumerable<DepartmentMember>, IEnumerable<DepartmentMemberViewModel>>(departmentMembers.Items);

        result.Result = new PagedResult<DepartmentMemberViewModel>(
            departmentMembersViewModel,
            departmentMembers.TotalCount,
            departmentMembers.CurrentPage,
            departmentMembers.PageSize
        );

        return result;
    }

    public async Task<DomainNotificationsResult<DepartmentMemberViewModel>> InsertDepartmentMember(Guid departmentGuid, string memberId)
    {
        var result = new DomainNotificationsResult<DepartmentMemberViewModel>();
        var churchId = _userContext.GetChurchId();
        var department = await _departmentRepo.GetDepartmentByGuid(churchId, departmentGuid);

        if (department == null)
        {
            result.Notifications.Add("Departamento não existe.");
            return result;
        }

        var member = await _repo.InsertDepartmentMember(department.Id, memberId);

        await _uow.Commit();

        result.Result = _mapper.Map<DepartmentMemberViewModel>(member);

        return result;
    }

    public async Task<DomainNotificationsResult<DepartmentMemberViewModel>> ToggleMemberStatus(Guid departmentGuid, string memberId)
    {
        var result = new DomainNotificationsResult<DepartmentMemberViewModel>();
        var churchId = _userContext.GetChurchId();
        var department = await _departmentRepo.GetDepartmentByGuid(churchId, departmentGuid);

        if (department == null)
        {
            result.Notifications.Add("Departamento não existe.");
            return result;
        }

        var member = await _repo.GetMemberById(churchId, department.Id, memberId);

        if(member == null)
        {
            result.Notifications.Add("Membro não encontrado.");
            return result;
        }

        member.Status = !member.Status;

        await _uow.Commit();

        result.Result = _mapper.Map<DepartmentMemberViewModel>(member);

        return result;
    }
}
