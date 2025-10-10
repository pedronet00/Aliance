using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.DTOs;

public class IncomeDTO
{
    public Guid Guid{ get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime? Date { get; set; } 
    public FinancialIncomingCategory Category { get; set; }
    public int? AccountReceivableId { get; set; }
    public int ChurchId { get; set; }
}
