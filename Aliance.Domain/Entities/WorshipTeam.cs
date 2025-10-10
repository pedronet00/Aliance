using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities;

public class WorshipTeam
{
    public int Id { get; set; }
    public Guid Guid { get; private set; }

    public string Name { get; set; }

    public bool Status { get; set; }

    // Collections
    public ICollection<WorshipTeamMember> WorshipTeamMembers { get; set; } = new List<WorshipTeamMember>();
    public ICollection<WorshipTeamRehearsal> WorshipTeamRehearsals { get; set; } = new List<WorshipTeamRehearsal>();


    public WorshipTeam() {}

    public WorshipTeam(string name, bool status)
    {
        Guid = Guid.NewGuid();
        Name = name;
        Status = status;
    }


}
