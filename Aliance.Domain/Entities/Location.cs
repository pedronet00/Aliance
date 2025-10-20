using System;

namespace Aliance.Domain.Entities
{
    public class Location
    {
        public int Id { get; set; }

        public Guid Guid { get; private set; }

        public string? Name { get; set; }

        public bool Status { get; set; }

        public int ChurchId { get; set; }

        public ICollection<Service>? Services { get; set; } = new List<Service>();

        private Location() { }

        public Location(string? name, int churchId, bool status = true)
        {
            Guid = Guid.NewGuid(); 
            Name = name;
            ChurchId = churchId;
            Status = status;
        }
    }
}
