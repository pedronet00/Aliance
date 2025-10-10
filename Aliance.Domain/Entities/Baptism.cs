using System;

namespace Aliance.Domain.Entities;

public class Baptism
{
    public int Id { get; set; }

    public Guid Guid { get; private set; }

    public DateTime Date { get; set; }

    public string PastorId { get; set; }
    public ApplicationUser Pastor { get; set; } = null!;

    public string UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;

    public int ChurchId { get; set; }
    public Church Church { get; set; }

    private Baptism() { }

    public Baptism(DateTime date, string pastorId, string userId, int churchId)
    {
        Guid = Guid.NewGuid(); // garante que sempre terá um identificador único
        Date = date;
        PastorId = pastorId;
        UserId = userId;
        ChurchId = churchId;
    }
}
