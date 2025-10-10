using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public async Task<DomainNotificationsResult<IEnumerable<LocationViewModel>>> GetLocations()
    {
        var result = new DomainNotificationsResult<IEnumerable<LocationViewModel>>();
        var churchId = _userContext.GetChurchId();

        var locations = await _repo.GetLocations(churchId);

        result.Result = _mapper.Map<IEnumerable<LocationViewModel>>(locations);

        return result;
    }
}
