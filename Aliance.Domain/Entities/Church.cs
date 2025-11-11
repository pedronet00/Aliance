using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Aliance.Domain.Entities;

public class Church : BaseEntity
{
    public int Id { get; set; }

    public Guid Guid { get; private set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string CNPJ { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Country { get; set; }

    public bool Status { get; set; }

    public PaymentStatus PaymentStatus { get; set; }    

    public string? AsaasCustomerId { get; set; }
    public string? SubscriptionId { get; set; }

    public DateTime? NextDueDate { get; set; } // Data do próximo pagamento

    public DateTime? LastPaymentDate { get; set; } // Data do último pagamento realizado

    public string? BillingType { get; set; }

    public decimal? PlanValue { get; set; } // Valor da assinatura

    public DateTime? DateActivated { get; set; } // Quando o pagamento foi confirmado

    public DateTime? DateCanceled { get; set; } // Quando a assinatura foi cancelada

    // Collections
    public ICollection<Baptism> Baptisms { get; set; } = new List<Baptism>();
    public ICollection<Cell> Cells { get; set; } = new List<Cell>();
    public ICollection<CostCenter> CostCenter { get; set; } = new List<CostCenter>();
    public ICollection<Department> Departments { get; set; } = new List<Department>();
    public ICollection<Event> Events { get; set; } = new List<Event>();
    public ICollection<Location> Locations { get; set; } = new List<Location>();
    public ICollection<Mission> Missions { get; set; } = new List<Mission>();
    public ICollection<MissionCampaign> MissionCampaigns { get; set; } = new List<MissionCampaign>();
    public ICollection<PastoralVisit> PastoralVisits { get; set; } = new List<PastoralVisit>();
    public ICollection<Patrimony> Patrimonies { get; set; } = new List<Patrimony>();
    public ICollection<SundaySchoolClassroom> SundaySchoolClassrooms { get; set; } = new List<SundaySchoolClassroom>();
    public ICollection<Service> Services { get; set; } = new List<Service>();

    private Church() { }

    public Church(
    string name,
    string email,
    string phone,
    string address,
    string city,
    string state,
    string country,
    string cnpj,
    PaymentStatus paymentStatus,
    string asaasCustomerId,
    string subscriptionId,
    DateTime? nextDueDate,
    DateTime? lastPaymentDate,
    string billingType,
    decimal planValue,
    DateTime? dateActivated,
    DateTime? dateCanceled,
    bool status = true
)
    {
        Guid = Guid.NewGuid();
        Name = name;
        Email = email;
        Phone = phone;
        Address = address;
        City = city;
        State = state;
        Country = country;
        CNPJ = cnpj;
        PaymentStatus = paymentStatus;
        AsaasCustomerId = asaasCustomerId;
        SubscriptionId = subscriptionId;
        NextDueDate = nextDueDate;
        LastPaymentDate = lastPaymentDate;
        BillingType = billingType;
        PlanValue = planValue;
        DateActivated = dateActivated;
        DateCanceled = dateCanceled;
        Status = status;
    }

}
