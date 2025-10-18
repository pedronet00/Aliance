using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.ViewModel.Auth;

public class NewClientViewModel
{
    public string ChurchName { get; set; }
    public string ChurchEmail { get; set; }
    public string ChurchPhone { get; set; }
    public string ChurchAddress { get; set; }
    public string ChurchCity { get; set; }
    public string ChurchState { get; set; }
    public string ChurchCountry { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string UserPhone { get; set; }
    public string Plan { get; set; }
    public string PaymentMethod { get; set; }

    // Dados do Asaas
    public string AsaasCustomerId { get; set; }
    public string AsaasSubscriptionId { get; set; }
    public decimal PlanValue { get; set; }
    public string SubscriptionStatus { get; set; }

    public string CheckoutUrl { get; set; }
}
