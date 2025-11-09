using Aliance.Application.ViewModel;
using Aliance.Domain.Notifications;

namespace Aliance.Application.Interfaces;

public interface ICellMemberService
{
    Task<DomainNotificationsResult<IEnumerable<CellMemberViewModel>>> GetCellMembers(Guid cellGuid);

    Task<DomainNotificationsResult<CellMemberViewModel>> InsertCellMember(Guid cellGuid, string memberId);

    Task<DomainNotificationsResult<bool>> DeleteCellMember(Guid cellGuid, string memberId);

    Task<DomainNotificationsResult<CellMemberViewModel>> ToggleMemberStatus(Guid cellGuid, string memberId);

}
