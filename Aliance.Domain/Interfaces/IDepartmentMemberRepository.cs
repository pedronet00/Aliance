using Aliance.Domain.Entities;
using Aliance.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface IDepartmentMemberRepository
{
    Task<PagedResult<DepartmentMember>> GetDepartmentMembers(int churchId, int departmentId, int pageNumber, int pageSize);

    Task<DepartmentMember> InsertDepartmentMember(int departmentId, string memberId);

    Task<DepartmentMember> GetMemberById(int churchId, int departmentId, string memberId);

    Task<DepartmentMember> DeleteDepartmentMember(int departmentId, string memberId);
}
