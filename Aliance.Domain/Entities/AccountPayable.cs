using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities;

public class AccountPayable
{
    public int Id { get; set; }

    public Guid Guid { get; set; } = Guid.NewGuid();    

    public string Description { get; set; }

    public decimal Amount { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? PaymentDate { get; set; }

    public AccountStatus AccountStatus { get; set; }

    public int CostCenterId { get; set; }

    public CostCenter CostCenter { get; set; }

    // Collections
    public ICollection<Expense>? Expenses { get; set; } = new List<Expense>();

    private AccountPayable() { }

    // Construtor para criação manual
    public AccountPayable(string description, decimal amount, DateTime dueDate, int costCenterId)
    {
        Guid = Guid.NewGuid();
        Description = description;
        Amount = amount;
        DueDate = dueDate;
        CostCenterId = costCenterId;
        AccountStatus = AccountStatus.Pendente;
    }

}
