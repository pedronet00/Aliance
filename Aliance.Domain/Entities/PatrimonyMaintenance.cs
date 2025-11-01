using Aliance.Domain.Enums;
using Aliance.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities;

public class PatrimonyMaintenance
{
    public int Id { get; set; }
    public Guid Guid { get; private set; } = Guid.NewGuid();
    public DateTime MaintenanceDate { get; set; }
    public string Description { get; set; }

    public Money MaintenanceCost { get; set; }
    public PatrimonyMaintenanceStatus Status { get; set; }
    public int PatrimonyId { get; set; }
    public Patrimony Patrimony { get; set; }
    public int CostCenterId { get; set; }
    public CostCenter? CostCenter { get; set; }

    public ICollection<PatrimonyMaintenanceDocument> Documents { get; private set; } = new List<PatrimonyMaintenanceDocument>();
    private PatrimonyMaintenance() { }
    public PatrimonyMaintenance(DateTime maintenanceDate, Money maintenanceCost, string description, int patrimonyId)
    {
        Guid = Guid.NewGuid();
        MaintenanceDate = maintenanceDate;
        MaintenanceCost = maintenanceCost;
        Description = description;
        PatrimonyId = patrimonyId;
        Status = PatrimonyMaintenanceStatus.Agendado;
    }

    public void AddDocument(PatrimonyMaintenanceDocument document)
    {
        if (document == null)
            throw new ArgumentNullException(nameof(document));

        Documents.Add(document);
    }
}
