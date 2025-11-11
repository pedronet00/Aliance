using Aliance.Domain.Enums;
using System;

namespace Aliance.Domain.Entities
{
    public class Cell : BaseEntity
    {
        public int Id { get; set; }

        public Guid Guid { get; private set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public string Name { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }

        public string LeaderId { get; set; }
        public ApplicationUser Leader { get; set; }

        public Weekdays MeetingDay { get; set; }

        public string CellBanner { get; set; }

        public int ChurchId { get; set; }
        public Church Church { get; set; }

        // Collections
        public ICollection<CellMember>? CellMembers { get; set; } = new List<CellMember>();
        public ICollection<CellMeeting>? CellMeetings { get; set; } = new List<CellMeeting>();

        private Cell() { }

        public Cell(string name, int locationId, string leaderId, Weekdays meetingDay, string cellBanner, int churchId)
        {
            Guid = Guid.NewGuid(); 
            Name = name;
            LocationId = locationId;
            LeaderId = leaderId;
            MeetingDay = meetingDay;
            CellBanner = cellBanner;
            ChurchId = churchId;
        }
    }
}
