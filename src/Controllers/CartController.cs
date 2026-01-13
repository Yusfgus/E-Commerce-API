using E_Commerce.Requests.Cart;
using E_Commerce.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers;

[ApiController]
[Route("api/cart")]
public class CartController(ICartService cartService) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> AddCartItem(CreateCartItemRequest request, CancellationToken ct)
    {
        var result = await cartService.AddCartItem(request, ct);

        return result.Match(
            onSuccess: _ => Created(),
            onFailure: Problem
        );
    }
}
