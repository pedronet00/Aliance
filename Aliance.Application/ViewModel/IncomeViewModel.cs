using Aliance.Domain.Enums;

namespace Aliance.Application.ViewModels;

public class IncomeViewModel
{
    public Guid Guid { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public FinancialIncomingCategory Category { get; set; }

    public int? AccountReceivableId { get; set; }
    public string? AccountReceivableName { get; set; } // opcional, se você quiser exibir o nome
    public int ChurchId { get; set; }
    public string ChurchName { get; set; } = string.Empty; // pode vir de navigation property
}
