using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;
using AutoMapper;

namespace Aliance.Application.Services;

public class LocationService : ILocationService
{

    private readonly ILocationRepository _repo;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContextService _userContext;

    public LocationService(ILocationRepository repo, IMapper mapper, IUnitOfWork unitOfWork, IUserContextService userContext)
    {
        _repo = repo;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<DomainNotificationsResult<PagedResult<LocationViewModel>>> GetLocations(int pageNumber, int pageSize)
    {
        var result = new DomainNotificationsResult<PagedResult<LocationViewModel>>();
        var churchId = _userContext.GetChurchId();

        var locations = await _repo.GetLocations(churchId, pageNumber, pageSize);

        var locationsVMs = _mapper.Map<IEnumerable<LocationViewModel>>(locations.Items);

        result.Result = new PagedResult<LocationViewModel>(
                locationsVMs,
                locations.TotalCount,
                locations.CurrentPage,
                locations.PageSize
            );

        return result;
    }

    public async Task<DomainNotificationsResult<LocationViewModel>> Insert(LocationDTO locationDTO)
    {
        var result = new DomainNotificationsResult<LocationViewModel>();
        var churchId = _userContext.GetChurchId();

        var location = _mapper.Map<Location>(locationDTO);

        await _repo.InsertLocation(location);

        await _unitOfWork.Commit();

        result.Result = _mapper.Map<LocationViewModel>(location);

        return result;
    }
}
