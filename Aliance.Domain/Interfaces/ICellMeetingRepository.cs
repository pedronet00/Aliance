using Aliance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface ICellMeetingRepository
{
    Task<IEnumerable<CellMeeting>> GetCellMeetings(int churchId, Guid cellGuid);

    Task<CellMeeting> GetCellMeetingByGuid(int churchId, Guid guid);

    Task<CellMeeting> InsertCellMeeting(CellMeeting cellMeeting);

    Task<CellMeeting> UpdateCellMeeting(int churchId, CellMeeting cellMeeting);

    Task<CellMeeting> DeleteCellMeeting(int churchId, Guid guid);

    Task<CellMeeting> GetNextCellMeeting(int churchId);

}
