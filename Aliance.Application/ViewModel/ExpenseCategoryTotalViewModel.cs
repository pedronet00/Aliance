using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.ViewModel;

public class ExpenseCategoryTotalViewModel
{
    public FinancialExpenseCategory Category { get; set; }
    public decimal Total { get; set; }
}
