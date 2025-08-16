namespace Aliance.Domain.Entities;

public class Budget
{
    public int Id { get; set; }
    public Guid Guid { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int CostCenterId { get; set; }
    public CostCenter CostCenter { get; set; }
    private Budget() { }
    
    public Budget(string name, decimal totalAmount, DateTime startDate, DateTime endDate, int costCenterId)
    {
        Guid = Guid.NewGuid();
        Name = name;
        TotalAmount = totalAmount;
        StartDate = startDate;
        EndDate = endDate;
        CostCenterId = costCenterId;
    }
}
