using Aliance.Domain.Enums;

namespace Aliance.Domain.Entities;

public class PastoralVisit
{
    public int Id { get; set; }
    public Guid Guid { get; private set; }
    public DateTime VisitDate { get; set; }
    public string Description { get; set; }
    public int VisitedMemberId { get; set; }
    public ApplicationUser VisitedMember { get; set; }
    public int PastorId { get; set; }
    public ApplicationUser Pastor { get; set; }

    public MeetingStatus Status { get; set; }
    public int ChurchId { get; set; }
    public Church Church { get; set; }
    private PastoralVisit() { }
    public PastoralVisit(DateTime visitDate, string description, int visitedMemberId, int churchId, int pastorId, MeetingStatus status)
    {
        Guid = Guid.NewGuid();
        VisitDate = visitDate;
        Description = description;
        VisitedMemberId = visitedMemberId;
        ChurchId = churchId;
        Status = status;
        PastorId = pastorId;
    }
}
