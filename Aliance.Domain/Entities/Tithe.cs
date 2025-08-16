using System;

namespace Aliance.Domain.Entities;

public class Tithe
{
    public int Id { get; set; }

    public Guid Guid { get; private set; }

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public int UserId { get; set; }

    public int ChurchId { get; set; }

    private Tithe() { }

    public Tithe(decimal amount, DateTime date, int userId, int churchId)
    {
        Guid = Guid.NewGuid(); 
        Amount = amount;
        Date = date;
        UserId = userId;
        ChurchId = churchId;
    }
}


