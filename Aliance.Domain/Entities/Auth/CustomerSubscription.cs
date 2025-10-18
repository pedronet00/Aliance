using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities.Auth;

public class CustomerSubscription
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public string ChurchId { get; set; } = null!;
    public string AsaasCustomerId { get; set; } = null!;
    public string AsaasSubscriptionId { get; set; } = null!;
    public SubscriptionPlan Plan { get; set; }
    public decimal Value { get; set; }
    public string Status { get; set; } = "PENDING"; // PENDING, ACTIVE, CANCELLED, OVERDUE
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ExpirationDate { get; set; }
}


