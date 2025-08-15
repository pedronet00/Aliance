using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities;

public class Church
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Country { get; set; }

    public bool Status { get; set; }

    public ICollection<Cell> Cells { get; set; } = new List<Cell>();
    public ICollection<Baptism> Baptisms { get; set; } = new List<Baptism>();
    public ICollection<Department> Departments { get; set; } = new List<Department>();
    public ICollection<Event> Events { get; set; } = new List<Event>();
    public ICollection<Location> Locations { get; set; } = new List<Location>();
    public ICollection<MissionCampaign> MissionCampaigns { get; set; } = new List<MissionCampaign>();
    public ICollection<CostCenter> CostCenter { get; set; } = new List<CostCenter>();


}
