namespace Aliance.Domain.Entities
{
    public class Department
    {
        public int Id { get; set; }

        public Guid Guid { get; private set; }

        public string Name { get; set; }

        public bool Status { get; set; }

        public int ChurchId { get; set; }
        public Church Church { get; set; }

        public ICollection<CostCenter> CostCenters { get; set; } = new List<CostCenter>();
        public ICollection<DepartmentMember> Members { get; set; } = new List<DepartmentMember>();

        private Department() { }

        public Department(string name, int churchId)
        {
            Guid = Guid.NewGuid(); 
            Name = name;
            ChurchId = churchId;
            Status = true;
        }
    }
}
