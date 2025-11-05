using Aliance.Domain.Enums;
using Aliance.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities;

public class AutomaticAccounts
{
    public int Id { get; set; }

    public Guid Guid { get; set; } = Guid.NewGuid();

    public AccountType AccountType { get; set; }

    public string Description { get; set; }

    public Money Amount { get; set; }

    public int DueDay { get; set; }

    public AccountStatus AccountStatus { get; set; }

    public int CostCenterId { get; set; }

    public CostCenter CostCenter { get; set; }

    public int ChurchId { get; set; }

    public Church Church { get; set; }
}
