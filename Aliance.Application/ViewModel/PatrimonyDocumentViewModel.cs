namespace Aliance.Application.ViewModel;

public class PatrimonyDocumentViewModel
{
    public Guid Guid { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; }
}
