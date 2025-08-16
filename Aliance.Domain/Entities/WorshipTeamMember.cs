using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities;

public class WorshipTeamMember
{
    public int Id { get; set; }

    public Guid Guid { get; private set; }

    public int UserId { get; set; }
    public ApplicationUser User { get; set; }

    public int WorshipTeamId { get; set; }
    public WorshipTeam WorshipTeam { get; set; }

    public bool Status { get; set; }

    private WorshipTeamMember() { }

    public WorshipTeamMember(int userId, int worshipTeamId, bool status)
    {
        UserId = userId;
        WorshipTeamId = worshipTeamId;
        Guid = Guid.NewGuid();
        Status = status;
    }
}
