using Aliance.Application.ViewModel;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface IDepartmentMemberService
{
    Task<DomainNotificationsResult<PagedResult<DepartmentMemberViewModel>>> GetDepartmentMembers(Guid departmentGuid, int pageNumber, int pageSize);

    Task<DomainNotificationsResult<DepartmentMemberViewModel>> InsertDepartmentMember(Guid departmentGuid, string memberId);

    Task<DomainNotificationsResult<DepartmentMemberViewModel>> DeleteDepartmentMember(Guid departmentGuid, string memberId);

    Task<DomainNotificationsResult<DepartmentMemberViewModel>> ToggleMemberStatus(Guid departmentGuid, string memberId);
}
