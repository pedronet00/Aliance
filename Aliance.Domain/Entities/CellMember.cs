namespace Aliance.Domain.Entities;

public class CellMember
{
    public int Id { get; set; }
    public Guid Guid { get; set; } = Guid.NewGuid();
    public int CellId { get; set; }
    public Cell Cell { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public DateTime JoinedDate { get; set; } = DateTime.UtcNow;
    public bool Status { get; set; } = true; 
    private CellMember() { }
    public CellMember(int cellId, string userId, bool status)
    {
        CellId = cellId;
        UserId = userId;
        Status = status;
        Guid = Guid.NewGuid();
    }
}
