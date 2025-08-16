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

        public ICollection<CostCenter> CostCenter { get; set; } = new List<CostCenter>();

        private Department() { }

        public Department(string name, int churchId, bool status = true)
        {
            Guid = Guid.NewGuid(); 
            Name = name;
            ChurchId = churchId;
            Status = status;
        }
    }
}
