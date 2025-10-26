using Aliance.Domain.Entities;
using Aliance.Domain.Pagination;

namespace Aliance.Domain.Interfaces;

public interface IWorshipTeamMemberRepository
{
    Task<PagedResult<WorshipTeamMember>> GetWorshipTeamMembers(int churchId, int teamId, int pageNumber, int pageSize);

    Task<WorshipTeamMember> InsertWorshipTeamMember(int teamId, string memberId);

    Task<WorshipTeamMember> DeleteWorshipTeamMember(int teamId, string memberId);

    Task<WorshipTeamMember> ToggleMemberStatus(int churchId, int teamId, string memberId, bool status);
}
