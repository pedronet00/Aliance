using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.ViewModel;

public class WorshipTeamMemberViewModel
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public Guid TeamGuid { get; set; }
    public Guid UserId { get; set; }

    public string UserName { get; set; }

    public bool Status { get; set; }
}
