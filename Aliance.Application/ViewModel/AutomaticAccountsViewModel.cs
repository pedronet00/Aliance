using Aliance.Domain.Enums;
using Aliance.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.ViewModel;

public class AutomaticAccountsViewModel
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public AccountType AccountType { get; set; }

    public string Description { get; set; }

    public decimal Amount { get; set; }

    public int DueDay { get; set; }

    public AccountStatus AccountStatus { get; set; }

    public int CostCenterId { get; set; }
    public string CostCenterName { get; set; }

    public int ChurchId { get; set; }
}
