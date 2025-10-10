using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.ViewModel;

public class ExpenseViewModel
{
    public Guid Guid { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public FinancialExpenseCategory Category { get; set; }
    public int? AccountReceivableId { get; set; }
    public string? AccountReceivableName { get; set; } // opcional, se você quiser exibir o nome
    public int ChurchId { get; set; }
}
