using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.ViewModel;

public class DepartmentMemberViewModel
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public Guid DepartmentGuId { get; set; }
    public Guid UserId { get; set; }

    public string UserName { get; set; }

    public bool Status { get; set; }
}
