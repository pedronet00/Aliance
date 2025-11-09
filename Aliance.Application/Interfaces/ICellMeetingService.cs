using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Notifications;

namespace Aliance.Application.Interfaces;

public interface ICellMeetingService
{
    Task<DomainNotificationsResult<IEnumerable<CellMeetingViewModel>>> GetCellMeetings(Guid cellGuid);

    Task<DomainNotificationsResult<CellMeetingViewModel>> GetCellMeetingByGuid(Guid guid);

    Task<DomainNotificationsResult<CellMeetingViewModel>> ToggleStatus(Guid guid, MeetingStatus status);

    Task<DomainNotificationsResult<CellMeetingViewModel>> InsertCellMeeting(CellMeetingDTO cellMeeting);

    Task<DomainNotificationsResult<CellMeetingViewModel>> UpdateCellMeeting(CellMeetingDTO cellMeeting);

    Task<DomainNotificationsResult<CellMeetingViewModel>> DeleteCellMeeting(Guid guid);

    Task<DomainNotificationsResult<CellMeetingViewModel>> GetNextCellMeeting();
}
