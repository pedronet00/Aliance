using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface IWorshipTeamMemberService
{
    Task<DomainNotificationsResult<PagedResult<WorshipTeamMemberViewModel>>> GetWorshipTeamMembers(Guid teamGuid, int pageNumber, int pageSize);

    Task<DomainNotificationsResult<WorshipTeamMemberViewModel>> InsertWorshipTeamMember(Guid teamGuid, string memberId);

    Task<DomainNotificationsResult<WorshipTeamMemberViewModel>> DeleteWorshipTeamMember(Guid teamGuid, string memberId);

    Task<DomainNotificationsResult<WorshipTeamMemberViewModel>> ToggleMemberStatus(Guid teamGuid, string memberId);
}
