using Aliance.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Services;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int GetChurchId()
    {
        var empresaId = _httpContextAccessor.HttpContext?.User?.FindFirst("ChurchId")?.Value;

        if (string.IsNullOrEmpty(empresaId))
            throw new UnauthorizedAccessException("ChurchId não localizada no token JWT.");

        return int.Parse(empresaId);
    }

    public string GetUserName()
    {
        return _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "Usuário desconhecido";
    }
}
