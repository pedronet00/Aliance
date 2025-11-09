using Aliance.Domain.Entities;

namespace Aliance.Domain.Interfaces;

public interface ICellMemberRepository
{
    Task<IEnumerable<CellMember>> GetCellMembers(int churchId, Guid cellGuid);

    Task<CellMember> InsertCellMember(Guid cellGuid, string memberId);

    Task<CellMember> GetMemberById(int churchId, Guid cellGuid, string memberId);

    Task<CellMember> DeleteCellMember(Guid cellGuid, string memberId);

    Task<CellMember> ToggleMemberStatus(int churchId, Guid cellGuid, string memberId, bool status);
}
