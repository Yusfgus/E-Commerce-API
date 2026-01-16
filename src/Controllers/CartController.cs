using System.Security.Claims;
using E_Commerce.Models.Auth;
using E_Commerce.Requests.Cart;
using E_Commerce.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers;

[ApiController]
[Route("api/cart")]
[Authorize(Roles = nameof(UserRole.Customer))]
public class CartController(ICartService cartService) : ApiController
{
    [HttpPost("items")]
    public async Task<IActionResult> AddCartItem(AddCartItemRequest request, CancellationToken ct)
    {
        Guid customerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await cartService.AddCartItemAsync(request, customerId, ct);

        return result.Match(
            onSuccess: cartItem => CreatedAtAction(nameof(GetCartItemById), new { id = cartItem.Id }, cartItem),
            onFailure: Problem
        );
    }
    
    [HttpDelete("items/{cartItemId}")]
    public async Task<IActionResult> RemoveCartItem(Guid cartItemId, CancellationToken ct)
    {
        Guid customerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await cartService.RemoveCartItemAsync(cartItemId, ct);

        return result.Match(
            onSuccess: NoContent,
            onFailure: Problem
        );
    }

    [HttpGet("items/{cartItemId}")]
    public async Task<IActionResult> GetCartItemById(Guid cartItemId, CancellationToken ct)
    {
        var result = await cartService.GetCartItemByIdAsync(cartItemId, ct);

        return result.Match(
            onSuccess: cartItem => Ok(cartItem),
            onFailure: Problem
        );
    }

    [HttpGet("items")]
    public async Task<IActionResult> GetCartItems(CancellationToken ct)
    {
        Guid customerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await cartService.GetCartItemsAsync(customerId, ct);

        return result.Match(
            onSuccess: cartItems => Ok(cartItems),
            onFailure: Problem
        );
    }
}
