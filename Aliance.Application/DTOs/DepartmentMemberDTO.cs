using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.DTOs;

public class DepartmentMemberDTO
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid UserId { get; set; }
}
