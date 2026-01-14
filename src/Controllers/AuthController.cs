using System.Security.Claims;
using E_Commerce.Models.Auth;
using E_Commerce.Request.Auth;
using E_Commerce.Requests.Auth;
using E_Commerce.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IAuthService authService) : ApiController
{
    [HttpPost("register/customer")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerRequest request, CancellationToken ct = default)
    {
        var result = await authService.RegisterCustomerAsync(request, ct);

        return result.Match(
            onSuccess: user => Ok(user),
            onFailure: Problem
        );
    }

    [HttpPost("register/vendor")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterVendor([FromBody] RegisterVendorRequest request, CancellationToken ct = default)
    {
        var result = await authService.RegisterVendorAsync(request, ct);

        return result.Match(
            onSuccess: user => Ok(user),
            onFailure: Problem
        );
    }

    [HttpPost("register/admin")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminRequest request, CancellationToken ct = default)
    {
        var result = await authService.RegisterAdminAsync(request, ct);

        return result.Match(
            onSuccess: user => Ok(user),
            onFailure: Problem
        );
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LogIn([FromBody] LogInUserRequest request, CancellationToken ct = default)
    {
        var result = await authService.LogInAsync(request, ct);
        
        return result.Match(
            onSuccess: user => Ok(user),
            onFailure: Problem
        );
    }

    [HttpPost("logout")]
    [Authorize]
    public IActionResult LogOut(CancellationToken ct = default)
    {
        // revoke refresh token
        return Ok("logOut");
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request, CancellationToken ct = default)
    {
        var result = await authService.Refresh(request, ct);
        
        return result.Match(
            onSuccess: token => Ok(token),
            onFailure: Problem
        );
    }

    [HttpGet("private-data")]
    [Authorize]
    public IActionResult GetPrivateData(CancellationToken ct = default)
    {
        return Ok(new
        {
            Id = User.FindFirstValue(ClaimTypes.NameIdentifier),
            Email = User.FindFirstValue(ClaimTypes.Email),
            Role = User.FindFirstValue(ClaimTypes.Role)
        });
    }

}
