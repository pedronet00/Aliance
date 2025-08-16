namespace Aliance.Domain.Entities;

public class Income
{
    public int Id { get; set; }
    public Guid Guid { get; private set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }

    public int? AccountReceivableId { get; set; }
    public AccountReceivable? AccountReceivable { get; set; }

    private Income() { }
    public Income(string description, decimal amount, int? accountReceivableId = null)
    {
        Description = description;
        Amount = amount;
        Guid = Guid.NewGuid();
        AccountReceivableId = accountReceivableId;
        Date = DateTime.UtcNow;
    }
}
