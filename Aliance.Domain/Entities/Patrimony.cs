using Aliance.Domain.Enums;

namespace Aliance.Domain.Entities;

public class Patrimony
{
    public int Id { get; set; }
    public Guid Guid { get; private set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal UnitValue { get; set; }
    public int Quantity { get; set; }
    public decimal TotalValue { get; set; }
    public DateTime AcquisitionDate { get; set; }
    public PatrimonyCondition Condition { get; set; }
    public int ChurchId { get; set; }
    public Church Church { get; set; }

    public ICollection<PatrimonyDocument> Documents { get; private set; } = new List<PatrimonyDocument>();

    // Collections
    public ICollection<PatrimonyMaintenance> Maintenances { get; set; } = new List<PatrimonyMaintenance>();
    private Patrimony() { }
    public Patrimony(int id, string name, string description, PatrimonyCondition patrimonyCondition, decimal unitValue, DateTime acquisitionDate, int churchId, decimal totalValue, int quantity)
    {
        Id = id;
        Guid = Guid.NewGuid();
        Name = name;
        Description = description;
        Condition = patrimonyCondition;
        UnitValue = unitValue;
        AcquisitionDate = acquisitionDate;
        ChurchId = churchId;
        Quantity = quantity;
        TotalValue = unitValue * quantity;
    }

    public void AddDocument(PatrimonyDocument document)
    {
        if (document == null)
            throw new ArgumentNullException(nameof(document));

        Documents.Add(document);
    }
}
