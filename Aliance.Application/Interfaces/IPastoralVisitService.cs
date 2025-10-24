using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface IPastoralVisitService
{
    Task<DomainNotificationsResult<PagedResult<PastoralVisitViewModel>>> GetAllVisits(int pageNumber, int pageSize);

    Task<DomainNotificationsResult<PastoralVisitViewModel>> GetPastoralVisitByGuid(Guid guid);

    Task<DomainNotificationsResult<PastoralVisitViewModel>> AddPastoralVisit(PastoralVisitDTO visit);

    Task<DomainNotificationsResult<PastoralVisitViewModel>> UpdatePastoralVisit(PastoralVisitDTO visit);

    Task<DomainNotificationsResult<PastoralVisitViewModel>> DeletePastoralVisit(Guid guid);

    Task<DomainNotificationsResult<PastoralVisitViewModel>> TogglePastoralVisitStatus(Guid guid, MeetingStatus status);
}
