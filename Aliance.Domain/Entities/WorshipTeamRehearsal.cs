using Aliance.Domain.Enums;

namespace Aliance.Domain.Entities;

public class WorshipTeamRehearsal
{
    public int Id { get; set; }
    public Guid Guid { get; private set; }
    public DateTime RehearsalDate { get; set; }
    public int WorshipTeamId { get; set; }
    public WorshipTeam WorshipTeam { get; set; }

    public MeetingStatus Status { get; set; }
    public WorshipTeamRehearsal() { }
    protected WorshipTeamRehearsal(DateTime rehearsalDate, int worshipTeamId, MeetingStatus status)
    {
        Guid = Guid.NewGuid();
        RehearsalDate = rehearsalDate;
        WorshipTeamId = worshipTeamId;
        Status = status;
    }
}
