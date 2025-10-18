using System;
using System.Collections.Generic;

namespace Aliance.Domain.Entities
{
    public class Church
    {
        public int Id { get; set; }

        public Guid Guid { get; private set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string CNPJ { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public bool Status { get; set; }

        // Collections
        public ICollection<Baptism> Baptisms { get; set; } = new List<Baptism>();
        public ICollection<Cell> Cells { get; set; } = new List<Cell>();
        public ICollection<CostCenter> CostCenter { get; set; } = new List<CostCenter>();
        public ICollection<Department> Departments { get; set; } = new List<Department>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
        public ICollection<Location> Locations { get; set; } = new List<Location>();
        public ICollection<Mission> Missions { get; set; } = new List<Mission>();
        public ICollection<MissionCampaign> MissionCampaigns { get; set; } = new List<MissionCampaign>();
        public ICollection<PastoralVisit> PastoralVisits { get; set; } = new List<PastoralVisit>();
        public ICollection<Patrimony> Patrimonies { get; set; } = new List<Patrimony>();
        public ICollection<SundaySchoolClassroom> SundaySchoolClassrooms { get; set; } = new List<SundaySchoolClassroom>();

        private Church() { }

        public Church(string name, string email, string phone, string address, string city, string state, string country, string cnpj, bool status = true)
        {
            Guid = Guid.NewGuid();
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            City = city;
            State = state;
            Country = country;
            Status = status;
            CNPJ = cnpj;
        }
    }
}
