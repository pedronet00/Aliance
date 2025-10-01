using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.Application.DTOs;

public class PatrimonyDocumentDTO
{
    public Guid PatrimonyGuid { get; set; }

    [FromForm]
    public IFormFile File { get; set; } = null!;
}
