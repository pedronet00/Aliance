using Aliance.Domain.Enums;
using Aliance.Domain.ValueObjects;

namespace Aliance.Domain.Entities;

public class Income : BaseEntity
{
    public int Id { get; set; }
    public Guid Guid { get; private set; } = Guid.NewGuid();
    public string Description { get; set; }
    public Money Amount { get; set; }
    public DateTime Date { get; set; }
    public FinancialIncomingCategory Category { get; set; }
    public int? AccountReceivableId { get; set; }
    public AccountReceivable? AccountReceivable { get; set; }

    public int ChurchId { get; set; }

    public Church Church { get; set; }

    private Income() { }
    public Income(string description, Money amount, int? accountReceivableId = null, FinancialIncomingCategory category = default, int churchId = 0)
    {
        Description = description;
        Amount = amount;
        Guid = Guid.NewGuid();
        AccountReceivableId = accountReceivableId;
        Date = DateTime.UtcNow;
        Category = category;
        ChurchId = churchId;
    }
}
