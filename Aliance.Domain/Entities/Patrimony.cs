using Aliance.Domain.Enums;

namespace Aliance.Domain.Entities;

public class Patrimony
{
    public int Id { get; set; }
    public Guid Guid { get; private set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Value { get; set; }
    public DateTime AcquisitionDate { get; set; }
    public PatrimonyCondition Condition { get; set; }
    public int ChurchId { get; set; }
    public Church Church { get; set; }
    private Patrimony() { }
    public Patrimony(int id, Guid guid, string name, string description, PatrimonyCondition patrimonyCondition, decimal value, DateTime acquisitionDate, int churchId)
    {
        Id = id;
        Guid = Guid.NewGuid();
        Name = name;
        Description = description;
        Condition = patrimonyCondition;
        Value = value;
        AcquisitionDate = acquisitionDate;
        ChurchId = churchId;
    }
}
