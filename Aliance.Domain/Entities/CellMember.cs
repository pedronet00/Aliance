namespace Aliance.Domain.Entities;

public class CellMember
{
    public int Id { get; set; }
    public Guid Guid { get; set; } = Guid.NewGuid();

    public Guid CellGuid { get; set; }    // FK baseado em Guid
    public Cell Cell { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public DateTime JoinedDate { get; set; } = DateTime.UtcNow;
    public bool Status { get; set; } = true;

    private CellMember() { }

    public CellMember(Guid cellGuid, string userId, bool status)
    {
        CellGuid = cellGuid;
        UserId = userId;
        Status = status;
        Guid = Guid.NewGuid();
    }
}
