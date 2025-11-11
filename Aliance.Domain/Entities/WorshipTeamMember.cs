namespace Aliance.Domain.Entities;

public class WorshipTeamMember : BaseEntity
{
    public int Id { get; set; }

    public Guid Guid { get; private set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public int WorshipTeamId { get; set; }
    public WorshipTeam WorshipTeam { get; set; }

    public bool Status { get; set; }

    private WorshipTeamMember() { }

    public WorshipTeamMember(string userId, int worshipTeamId, bool status)
    {
        UserId = userId;
        WorshipTeamId = worshipTeamId;
        Guid = Guid.NewGuid();
        Status = status;
    }
}
