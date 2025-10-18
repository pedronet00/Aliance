using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.DTOs.Auth;

public class CreateSubscriptionDTO
{
    public string UserId { get; set; } = null!;
    public string ChurchId { get; set; } = null!;
    public SubscriptionPlan Plan { get; set; }
}

