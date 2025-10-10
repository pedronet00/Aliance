using Aliance.Domain.Enums;

namespace Aliance.Domain.Entities;

public class CellMeeting
{
    public int Id { get; set; }                       
    public Guid Guid { get; set; } = Guid.NewGuid();

    public string Theme { get; set; }
    public DateTime Date { get; set; }

    // Relacionamentos via GUID
    public Guid LocationGuid { get; set; }
    public Location Location { get; set; }

    public Guid CellGuid { get; set; }
    public Cell Cell { get; set; }

    public string LeaderGuid { get; set; }
    public ApplicationUser Leader { get; set; }

    public MeetingStatus Status { get; set; } = MeetingStatus.Agendado;

    public CellMeeting() { } 

}
