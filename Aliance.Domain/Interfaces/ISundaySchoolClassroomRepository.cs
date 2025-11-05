using Aliance.Domain.Entities;
using Aliance.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces;

public interface ISundaySchoolClassroomRepository
{
    Task<PagedResult<SundaySchoolClassroom>> GetSundaySchoolClassrooms(int churchId, int pageNumber, int pageSize);

    Task<SundaySchoolClassroom> GetSundaySchoolClassroomByGuid(int churchId, Guid guid);

    Task<SundaySchoolClassroom> InsertSundaySchoolClassroom(SundaySchoolClassroom location);
}
