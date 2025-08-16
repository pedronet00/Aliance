using Aliance.Domain.Enums;

namespace Aliance.Domain.Entities;

public class CellMeeting
{
    public int Id { get; set; }

    public Guid Guid { get; set; } = Guid.NewGuid();

    public string Theme { get; set; }

    public DateTime Date { get; set; }

    public int LocationId { get; set; }
    public Location Location { get; set; }

    public int CellId { get; set; }
    public Cell Cell { get; set; }

    public int LeaderId { get; set; }
    public ApplicationUser Leader { get; set; }

    public MeetingStatus Status { get; set; } = MeetingStatus.Scheduled;

    private CellMeeting() { }

    public CellMeeting(string theme, DateTime date, int locationId, int cellId, int leaderId)
    {
        Guid = Guid.NewGuid();
        Theme = theme;
        Date = date;
        LocationId = locationId;
        CellId = cellId;
        LeaderId = leaderId;
    }
}
