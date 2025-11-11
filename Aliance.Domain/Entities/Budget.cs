using Aliance.Domain.Enums;
using Aliance.Domain.ValueObjects;

namespace Aliance.Domain.Entities;

public class Budget : BaseEntity
{
    public int Id { get; set; }
    public Guid Guid { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Description { get; set; }
    public Money TotalAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public BudgetStatus Status { get; set; }
    public int CostCenterId { get; set; }
    public CostCenter CostCenter { get; set; }
    private Budget() { }

    public Budget(string name, Money totalAmount, DateTime startDate, DateTime endDate, int costCenterId, string description, BudgetStatus status)
    {
        Guid = Guid.NewGuid();
        Name = name;
        TotalAmount = totalAmount;
        StartDate = startDate;
        EndDate = endDate;
        CostCenterId = costCenterId;
        Description = description;
        Status = BudgetStatus.PendenteAprovacao;
    }
}
