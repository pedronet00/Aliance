using Aliance.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
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
        var churchId = _httpContextAccessor.HttpContext?.User?.FindFirst("churchId")?.Value;

        if (string.IsNullOrEmpty(churchId))
            throw new UnauthorizedAccessException("churchId não localizada no token JWT.");

        return int.Parse(churchId);
    }

    public string GetUserName()
    {
        return _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "Usuário desconhecido";
    }
}
