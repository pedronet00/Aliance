namespace Aliance.Domain.Entities;

public class PatrimonyDocument : BaseEntity
{
    public int Id { get; private set; }
    public Guid Guid { get; private set; }
    public string FileName { get; private set; }
    public string ContentType { get; private set; }
    public string FilePath { get; private set; } 
    public DateTime UploadedAt { get; private set; }

    public int PatrimonyId { get; private set; }
    public Patrimony Patrimony { get; private set; }

    private PatrimonyDocument() { }

    public PatrimonyDocument(string fileName, string contentType, string filePath, int patrimonyId)
    {
        Guid = Guid.NewGuid();
        FileName = fileName;
        ContentType = contentType;
        FilePath = filePath;
        UploadedAt = DateTime.UtcNow;
        PatrimonyId = patrimonyId;
    }
}
