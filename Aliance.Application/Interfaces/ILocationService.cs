using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface ILocationService
{
    Task<DomainNotificationsResult<PagedResult<LocationViewModel>>> GetLocations(int pageNumber, int pageSize);

    Task<DomainNotificationsResult<LocationViewModel>> Insert(LocationDTO locationDTO);
}
