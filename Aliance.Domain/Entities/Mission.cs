namespace Aliance.Domain.Entities;

public class Mission : BaseEntity
{
    public int Id { get; set; }

    public Guid Guid { get; private set; }

    public string Name { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public bool Status { get; set; }

    public int ChurchId { get; set; }

    public Church Church { get; set; }

    // Collections
    public ICollection<MissionMember> MissionMembers { get; set; } = new List<MissionMember>();

    private Mission() { }

    public Mission(string name, string city, string state, bool status, int churchId)
    {
        Name = name;
        City = city;
        State = state;
        Status = status;
        Guid = Guid.NewGuid();
        ChurchId = churchId;
    }
}
