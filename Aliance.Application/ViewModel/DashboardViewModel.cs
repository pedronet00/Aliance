using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.ViewModel;

    public class DashboardViewModel
    {
        public IEnumerable<IncomeMonthlyTotalViewModel> IncomeTotals { get; set; }
        public IEnumerable<ExpenseMonthlyTotalViewModel> ExpenseTotals { get; set; }
        public int TotalUsers { get; set; }
        public int TotalBudgets { get; set; }
        public int TotalEvents { get; set; }
        public int TotalPatrimonies { get; set; }
    }

