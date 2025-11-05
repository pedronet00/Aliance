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

public interface ISundaySchoolClassroomService
{
    Task<DomainNotificationsResult<PagedResult<SundaySchoolClassroomViewModel>>> GetClassrooms(int pageNumber, int pageSize);

    Task<DomainNotificationsResult<SundaySchoolClassroomViewModel>> Insert(SundaySchoolClassroomDTO classroomDTO);
}
