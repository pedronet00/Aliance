using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface IPastoralVisitRepository
{
    Task<IEnumerable<PastoralVisit>> GetAllVisits(int churchId);

    Task<PastoralVisit> GetPastoralVisitByGuid(int churchId, Guid guid);

    Task<PastoralVisit> AddPastoralVisit(PastoralVisit visit);

    Task<PastoralVisit> UpdatePastoralVisit(int churchId, PastoralVisit visit);

    Task<PastoralVisit> DeletePastoralVisit(int churchId, Guid guid);

    Task<PastoralVisit> TogglePastoralVisitStatus(int churchId, Guid guid, MeetingStatus status);
}
