using Aliance.Domain.Enums;

namespace Aliance.Domain.Entities;

public class LeadershipMeetings : BaseEntity
{
    public int Id { get; set; }
    public Guid Guid { get; private set; } = Guid.NewGuid();
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime MeetingDate { get; set; }
    public MeetingStatus Status { get; set; }

    private LeadershipMeetings() { }
    public LeadershipMeetings(string title, string description, DateTime meetingDate)
    {
        Title = title;
        Description = description;
        MeetingDate = meetingDate;
        Status = MeetingStatus.Agendado; 
        Guid = Guid.NewGuid();
    }
}
