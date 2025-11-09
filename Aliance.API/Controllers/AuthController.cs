using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Aliance.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthController(ITokenService tokenService,
                          IConfiguration configuration,
                          UserManager<ApplicationUser> userManager,
                          RoleManager<IdentityRole> roleManager)
    {
        _tokenService = tokenService;
        _configuration = configuration;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email!);

        if (user is not null && await _userManager.CheckPasswordAsync(user, loginDto.Password!))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("churchId", user.ChurchId.ToString(), ClaimValueTypes.Integer32),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GenerateAccessToken(authClaims, _configuration);

            var refreshToken = _tokenService.GenerateRefreshToken();

            // Usar _ não aloca memória! 
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);

            user.RefreshToken = refreshToken;

            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityInMinutes);

            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                refreshToken = refreshToken
            });
        }

        return Unauthorized(new { message = "Invalid username or password." });
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] UserDTO registerDTO)
    {
        var userExists = await _userManager.FindByEmailAsync(registerDTO.Email!);

        if (userExists is not null)
        {
            return BadRequest(new { message = "User already exists." });
        }

        ApplicationUser user = new()
        {
            UserName = registerDTO.UserName,
            Email = registerDTO.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            ChurchId = registerDTO.ChurchId,
            Status = registerDTO.Status,
            PhoneNumber = registerDTO.PhoneNumber,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        var role = await _roleManager.RoleExistsAsync(registerDTO.Role!);

        if (!role)
            return BadRequest(new { message = "Role does not exist." });

        var result = await _userManager.CreateAsync(user, registerDTO.Password!);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, registerDTO.Role!);
        }
        else
        {
            return BadRequest(new { message = "User registration failed.", 
                                    errors = result.Errors.Select(e => e.Description) 
            });
        }

        return Ok(new {message = "User created successfully."});
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenDTO token)
    {
        if (token is null)
            return BadRequest(new { message = "Invalid client request." });

        string? accessToken = token.AccessToken ?? throw new ArgumentNullException(nameof(token));

        string? refreshToken = token.RefreshToken ?? throw new ArgumentNullException(nameof(token));

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken, _configuration);

        if (principal is null)
            return BadRequest(new { message = "Invalid access token." });

        string userName = principal.Identity.Name;

        var user = await _userManager.FindByNameAsync(userName!);

        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return BadRequest(new { message = "Invalid refresh token." });
        }

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims, _configuration);

        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;

        await _userManager.UpdateAsync(user);

        return new ObjectResult(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken = newRefreshToken,
        });
    }

    [Authorize]
    [HttpPost]
    [Route("revoke/{username}")]
    public async Task<IActionResult> Revoke(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user is null)
            return BadRequest("Invalid username");

        user.RefreshToken = null;

        await _userManager.UpdateAsync(user);

        return NoContent();
    }

    [HttpPost]
    [Route("CreateRole")]
    public async Task<IActionResult> CreateRole([FromBody] string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
            return BadRequest(new { message = "Role name cannot be empty." });
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (roleExists)
            return BadRequest(new { message = "Role already exists." });
        var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Role creation failed." });
        return Ok(new { message = "Role created successfully." });
    }

    [HttpPost]
    [Route("AddUserToRole")]
    public async Task<IActionResult> AddUserToRole(string email, string roleName)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(roleName))
            return BadRequest(new { message = "Email and role name cannot be empty." });

        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            return NotFound(new { message = "User not found." });

        var roleExists = await _roleManager.RoleExistsAsync(roleName);

        if (!roleExists)
            return BadRequest(new { message = "Role does not exist." });

        var result = await _userManager.AddToRoleAsync(user, roleName);

        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to add user to role." });

        return Ok(new { message = "User added to role successfully." });
    }
}
