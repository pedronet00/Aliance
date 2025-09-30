using Microsoft.AspNetCore.Http;

namespace Aliance.Application.DTOs;

public class PatrimonyDocumentDTO
{
    public Guid PatrimonyGuid { get; set; }
    public IFormFile File { get; set; } = null!;
}
