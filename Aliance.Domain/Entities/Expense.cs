namespace Aliance.Domain.Entities;

public class Expense
{
    public int Id { get; set; }
    public Guid Guid { get; private set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }

    public int? AccountPayableId { get; set; }
    public AccountPayable? AccountPayable { get; set; }

    private Expense() { }
    public Expense(string description, decimal amount, int? accountPayableId = null)
    {
        Description = description;
        Amount = amount;
        Guid = Guid.NewGuid();
        AccountPayableId = accountPayableId;
        Date = DateTime.UtcNow;
    }
}
