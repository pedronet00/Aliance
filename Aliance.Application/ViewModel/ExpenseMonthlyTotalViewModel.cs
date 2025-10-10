using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.ViewModel;

public class ExpenseMonthlyTotalViewModel
{
    public int Month { get; set; }           // ex: 1 = Janeiro
    public string MonthName { get; set; }    // "Janeiro"
    public decimal Total { get; set; }
}
