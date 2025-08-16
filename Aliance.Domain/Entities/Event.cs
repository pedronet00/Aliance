using System;

namespace Aliance.Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }

        public Guid Guid { get; private set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime Date { get; set; }

        public decimal Cost { get; set; }

        public int LocationId { get; set; }
        public Location? Location { get; set; }

        public int ChurchId { get; set; }

        private Event() { }

        public Event(string? name, string? description, DateTime date, decimal cost, int locationId, int churchId)
        {
            Guid = Guid.NewGuid(); 
            Name = name;
            Description = description;
            Date = date;
            Cost = cost;
            LocationId = locationId;
            ChurchId = churchId;
        }
    }
}
