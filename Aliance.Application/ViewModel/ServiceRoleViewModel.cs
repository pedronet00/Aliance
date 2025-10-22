using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.ViewModel;

public class ServiceRoleViewModel
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public int ServiceId { get; set; }
    public Guid MemberId { get; set; }
    public string MemberName { get; set; }
    public ServiceRoles Role { get; set; }
}
