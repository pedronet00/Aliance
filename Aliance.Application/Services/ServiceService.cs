using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Enums;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using AutoMapper;

namespace Aliance.Application.Services;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContextService _userContext;

    public ServiceService(IServiceRepository serviceRepository, IMapper mapper, IUnitOfWork unitOfWork, IUserContextService userContext)
    {
        _serviceRepository = serviceRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<DomainNotificationsResult<ServiceViewModel>> AddService(ServiceDTO serviceDTO)
    {
        var result = new DomainNotificationsResult<ServiceViewModel>();

        var validateServiceExists = await _serviceRepository.ServiceExists(serviceDTO.LocationId, serviceDTO.Date, _userContext.GetChurchId());

        if (validateServiceExists)
        {
            result.Notifications.Add("Já exite um culto nessa data, hora e local.");
            return result;
        }

        var serviceEntity = _mapper.Map<Domain.Entities.Service>(serviceDTO);

        await _serviceRepository.AddService(serviceEntity);

        await _unitOfWork.Commit();

        result.Result = _mapper.Map<ServiceViewModel>(serviceEntity);

        return result;
    }

    public async Task<DomainNotificationsResult<bool>> DeleteService(Guid serviceGuid)
    {
        var result = new DomainNotificationsResult<bool>();

        var churchId = _userContext.GetChurchId();

        var deleteResult = await _serviceRepository.DeleteService(serviceGuid, churchId);

        await _unitOfWork.Commit();

        result.Result = deleteResult;

        return result;
    }

    public async Task<DomainNotificationsResult<ServiceViewModel>> GetServiceByGuid(Guid serviceGuid)
    {
        var result = new DomainNotificationsResult<ServiceViewModel>();

        var churchId = _userContext.GetChurchId();

        var serviceEntity = await _serviceRepository.GetServiceByGuid(serviceGuid, churchId);

        result.Result = _mapper.Map<ServiceViewModel>(serviceEntity);

        return result;
    }

    public async Task<DomainNotificationsResult<IEnumerable<ServiceViewModel>>> GetServices()
    {
        var result = new DomainNotificationsResult<IEnumerable<ServiceViewModel>>();
        var churchId = _userContext.GetChurchId();

        var serviceEntities = await _serviceRepository.GetServices(churchId);
        result.Result = _mapper.Map<IEnumerable<ServiceViewModel>>(serviceEntities);

        return result;
    }

    public async Task<DomainNotificationsResult<ServiceViewModel>> ToggleStatus(Guid serviceGuid, ServiceStatus status)
    {
        var result = new DomainNotificationsResult<ServiceViewModel>();
        var churchId = _userContext.GetChurchId();

        var serviceEntity = await _serviceRepository.ToggleStatus(serviceGuid, churchId, status);

        await _unitOfWork.Commit();

        result.Result = _mapper.Map<ServiceViewModel>(serviceEntity);

        return result;
    }

    public async Task<DomainNotificationsResult<ServiceViewModel>> UpdateService(ServiceDTO serviceDTO)
    {
        var result = new DomainNotificationsResult<ServiceViewModel>();
        var serviceEntity = _mapper.Map<Domain.Entities.Service>(serviceDTO);

        await _serviceRepository.UpdateService(serviceEntity);

        await _unitOfWork.Commit();

        result.Result = _mapper.Map<ServiceViewModel>(serviceEntity);

        return result;
    }
}
