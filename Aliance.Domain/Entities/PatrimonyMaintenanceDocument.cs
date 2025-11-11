using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities;

[Table("patrimonymaintenancedocument")]
public class PatrimonyMaintenanceDocument : BaseEntity
{
    public int Id { get; private set; }
    public Guid Guid { get; private set; }
    public string FileName { get; private set; }
    public string ContentType { get; private set; }
    public string FilePath { get; private set; }
    public DateTime UploadedAt { get; private set; }

    public int MaintenanceId { get; private set; }
    public PatrimonyMaintenance Maintenance { get; private set; }

    private PatrimonyMaintenanceDocument() { }

    public PatrimonyMaintenanceDocument(string fileName, string contentType, string filePath, int maintenanceId)
    {
        Guid = Guid.NewGuid();
        FileName = fileName;
        ContentType = contentType;
        FilePath = filePath;
        UploadedAt = DateTime.UtcNow;
        MaintenanceId = maintenanceId;
    }
}
