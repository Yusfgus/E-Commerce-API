using E_Commerce.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController(IAdminService adminService) : ApiController
{
    
    [HttpGet("users")]
    public async Task<IActionResult> GetUser(Guid id, int page = 1, int pageSize = 10, CancellationToken ct = default)
    {
        var result = await adminService.GetUsersAsync(page, pageSize, ct);

        return result.Match(
            onSuccess: users => Ok(users),
            onFailure: Problem
        );
    }


    [HttpGet("users/{id:guid}")]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken ct = default)
    {
        var result = await adminService.GetUserByIdAsync(id, ct);

        return result.Match(
            onSuccess: user => Ok(user),
            onFailure: Problem
        );
    }


    [HttpDelete("users/{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken ct = default)
    {
        var result = await adminService.DeleteUserAsync(id, ct);
        
        return result.Match<IActionResult>(
            onSuccess: _ => NoContent(),
            onFailure: Problem
        );
    }


    [HttpPost("users/{id:guid}/activate")]
    public async Task<IActionResult> ActivateUser(Guid id, CancellationToken ct = default)
    {
        var result = await adminService.ActivateUserAsync(id, ct);
        
        return result.Match<IActionResult>(
            onSuccess: _ => NoContent(),
            onFailure: Problem
        );
    }


    [HttpPost("users/{id:guid}/deactivate")]
    public async Task<IActionResult> DeactivateUser(Guid id, CancellationToken ct = default)
    {
        var result = await adminService.DeactivateUserAsync(id, ct);
        
        return result.Match<IActionResult>(
            onSuccess: _ => NoContent(),
            onFailure: Problem
        );
    }
}