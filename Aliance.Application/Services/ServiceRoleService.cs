using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using AutoMapper;
using System;

namespace Aliance.Application.Services;

public class ServiceRoleService : IServiceRoleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IServiceRoleRepository _repo;
    private readonly IUserContextService _userContext;
    private readonly IServiceRepository _serviceRepo;

    public ServiceRoleService(IUnitOfWork unitOfWork, IMapper mapper, IServiceRoleRepository repo, IUserContextService userContext, IServiceRepository serviceRepo)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repo = repo;
        _userContext = userContext;
        _serviceRepo = serviceRepo;
    }

    public async Task<DomainNotificationsResult<ServiceRoleViewModel>> Add(ServiceRoleDTO serviceRoleDTO)
    {
        var result = new DomainNotificationsResult<ServiceRoleViewModel>();
        var churchId = _userContext.GetChurchId();
        var service = await _serviceRepo.GetServiceByGuid(serviceRoleDTO.ServiceGuid, churchId); 

        if(service is null)
        {
            result.Notifications.Add("Culto não encontrado");
            return result;
        }

        var serviceRole = _mapper.Map<ServiceRole>(serviceRoleDTO);

        serviceRole.Service = null;
        serviceRole.ServiceId = service.Id;

        await _repo.Add(serviceRole);

        await _unitOfWork.Commit();

        result.Result = _mapper.Map<ServiceRoleViewModel>(serviceRole);

        return result;
    }

    public async Task<DomainNotificationsResult<bool>> Delete(Guid serviceRoleGuid)
    {
        var result = new DomainNotificationsResult<bool>();
        var churchId = _userContext.GetChurchId();

        var deleteResult = await _repo.Delete(serviceRoleGuid, churchId);

        await _unitOfWork.Commit();

        result.Result = deleteResult;

        return result;
    }

    public async Task<DomainNotificationsResult<IEnumerable<ServiceRoleViewModel>>> List(Guid serviceGuid)
    {
        var result = new DomainNotificationsResult<IEnumerable<ServiceRoleViewModel>>();
        var churchId = _userContext.GetChurchId();
        var service = await _serviceRepo.GetServiceByGuid(serviceGuid, churchId);

        if (service is null)
        {
            result.Notifications.Add("Culto não encontrado");
            return result;
        }

        var serviceRoles = await _repo.List(service.Id);

        result.Result = _mapper.Map<IEnumerable<ServiceRoleViewModel>>(serviceRoles);

        return result;
    }

}
