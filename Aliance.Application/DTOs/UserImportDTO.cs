using Microsoft.AspNetCore.Http;

namespace Aliance.Application.DTOs;

public class UserImportDTO
{
    public IFormFile File { get; set; }
}
