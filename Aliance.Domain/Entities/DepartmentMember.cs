namespace Aliance.Domain.Entities;

public class DepartmentMember
{
    public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public DateTime JoinedDate { get; set; } = DateTime.UtcNow;
    public bool Status { get; set; } = true;
    private DepartmentMember() { }
    public DepartmentMember(int departmentId, string userId, bool status)
    {
        DepartmentId = departmentId;
        UserId = userId;
        Status = status;
        Guid = Guid.NewGuid();
    }
}
