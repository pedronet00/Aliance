using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.ViewModel;

public class AccountReceivableViewModel
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public string CostCenterName { get; set; }

    public string Description { get; set; }

    public decimal Amount { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? PaymentDate { get; set; }

    public AccountStatus AccountStatus { get; set; }

    public int CostCenterId { get; set; }
}
