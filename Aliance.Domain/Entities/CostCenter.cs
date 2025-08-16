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

        // Collections
        public ICollection<AccountPayable> AccountPayable { get; set; } = new List<AccountPayable>();

        private CostCenter() { }

        public CostCenter(string name, int departmentId, int churchId)
        {
            Guid = Guid.NewGuid(); 
            Name = name;
            DepartmentId = departmentId;
            ChurchId = churchId;
        }
    }
}
