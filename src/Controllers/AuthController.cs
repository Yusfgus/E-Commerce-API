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
    public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerRequest request, CancellationToken ct = default)
    {
        var result = await authService.RegisterCustomerAsync(request, ct);

        return result.Match(
            onSuccess: user => Ok(user),
            onFailure: Problem
        );
    }

    [HttpPost("register/vendor")]
    public async Task<IActionResult> RegisterVendor([FromBody] RegisterVendorRequest request, CancellationToken ct = default)
    {
        var result = await authService.RegisterVendorAsync(request, ct);

        return result.Match(
            onSuccess: user => Ok(user),
            onFailure: Problem
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] LogInUserRequest request, CancellationToken ct = default)
    {
        var result = await authService.LogInAsync(request, ct);
        
        return result.Match(
            onSuccess: user => Ok(user),
            onFailure: Problem
        );
    }

    [HttpPost("logout")]
    public IActionResult LogOut(CancellationToken ct = default)
    {
        return Ok("logOut");
    }

    [HttpPost("refresh")]
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
        return Ok("You are authenticated");
    }

}
