using Aliance.Domain.ValueObjects;
using System;

namespace Aliance.Domain.Entities;

public class Tithe
{
    public int Id { get; set; }

    public Guid Guid { get; private set; }

    public Money Amount { get; set; }

    public DateTime Date { get; set; }

    public string UserId { get; set; }

    public ApplicationUser User { get; set; }

    public int ChurchId { get; set; }

    public Church Church { get; set; }

    private Tithe() { }

    public Tithe(Money amount, DateTime date, string userId, int churchId)
    {
        Guid = Guid.NewGuid(); 
        Amount = amount;
        Date = date;
        UserId = userId;
        ChurchId = churchId;
    }
}


