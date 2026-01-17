using E_Commerce.Models.Auth;
using E_Commerce.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize(Roles = nameof(UserRole.Customer))]
public class OrderController(IOrderService orderService) : ApiController
{
    [HttpPost("/checkout")]
    public async Task<IActionResult> Checkout(CancellationToken ct = default)
    {
        Guid customerId = GetUserId();

        var result = await orderService.CheckoutAsync(customerId, ct);

        return result.Match(
            onSuccess: order => Ok(order),
            onFailure: Problem
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomerOrders(Guid customerId, int page = 1, int pageSize = 10, CancellationToken ct = default)
    {
        var result = await orderService.GetByCustomerIdAsync(customerId, page, pageSize, ct);

        return result.Match(
            onSuccess: orders => Ok(orders),
            onFailure: Problem
        );
    }

}