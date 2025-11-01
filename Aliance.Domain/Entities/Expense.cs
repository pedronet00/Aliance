using Aliance.Domain.Enums;
using Aliance.Domain.ValueObjects;

namespace Aliance.Domain.Entities;

public class Expense
{
    public int Id { get; set; }
    public Guid Guid { get; private set; } = Guid.NewGuid();
    public string Description { get; set; }
    public Money Amount { get; set; }
    public DateTime Date { get; set; }

    public FinancialExpenseCategory Category { get; set; }
    public int? AccountPayableId { get; set; }
    public AccountPayable? AccountPayable { get; set; }

    public int ChurchId { get; set; }

    public Church Church { get; set; }

    private Expense() { }
    public Expense(string description, Money amount, int? accountPayableId = null)
    {
        Description = description;
        Amount = amount;
        Guid = Guid.NewGuid();
        AccountPayableId = accountPayableId;
        Date = DateTime.UtcNow;
    }
}
