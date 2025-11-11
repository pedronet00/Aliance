using Aliance.Domain.Enums;
using Aliance.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities;

public class AccountReceivable : BaseEntity
{
    public int Id { get; set; }

    public Guid Guid { get; set; } = Guid.NewGuid();

    public string Description { get; set; }

    public Money Amount { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? PaymentDate { get; set; }

    public AccountStatus AccountStatus { get; set; }

    public int CostCenterId { get; set; }

    public CostCenter CostCenter { get; set; }

    // Collections
    public ICollection<Income>? Incomes { get; set; } = new List<Income>();

    private AccountReceivable() { }

    // Construtor para criação manual
    public AccountReceivable(string description, Money amount, DateTime dueDate, int costCenterId)
    {
        Guid = Guid.NewGuid();
        Description = description;
        Amount = amount;
        DueDate = dueDate;
        CostCenterId = costCenterId;
        AccountStatus = AccountStatus.Pendente;
    }
}
