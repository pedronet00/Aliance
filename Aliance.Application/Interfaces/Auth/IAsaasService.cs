using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces.Auth;

public interface IAsaasService
{
    Task<string> CreateCustomerAsync(Church church, ApplicationUser user);
    Task<(string subscriptionId, decimal value)> CreateSubscriptionAsync(string customerId, string plan, string paymentMethod);
    Task<string> GetCustomerPayments(string subscriptionId);
    Task<string> GetCustomerData(string customerId);
    Task<string> DeleteCustomerAsync(string customerId);
    Task<string> CreateCheckoutAsync(string customerId, string plan, string paymentMethod);
}
