namespace Aliance.Domain.Entities
{
    public class CostCenter
    {
        public int Id { get; set; }

        public Guid Guid { get; private set; }

        public string Name { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int ChurchId { get; set; }
        public Church Church { get; set; }

        public bool Status { get; set; }

        // Collections
        public ICollection<Budget>? Budget{ get; set; } = new List<Budget>();
        public ICollection<AccountPayable> AccountPayable { get; set; } = new List<AccountPayable>();
        public ICollection<AccountReceivable> AccountReceivable{ get; set; } = new List<AccountReceivable>();
        public ICollection<PatrimonyMaintenance>? PatrimonyMaintenance { get; set; } = new List<PatrimonyMaintenance>();

        public ICollection<Event> Event { get; set; } = new List<Event>();

        private CostCenter() { }

        public CostCenter(string name, int departmentId, int churchId)
        {
            Guid = Guid.NewGuid(); 
            Name = name;
            DepartmentId = departmentId;
            ChurchId = churchId;
            Status = true;
        }
    }
}
