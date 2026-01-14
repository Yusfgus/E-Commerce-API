using E_Commerce.Models.Auth;
using E_Commerce.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize(Roles = nameof(UserRole.Customer))]
public class OrderController(IOrderService OrderService) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetCustomerOrders(Guid customerId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
    {
        var result = await OrderService.GetByCustomerIdAsync(customerId, page, pageSize, ct);

        return result.Match(
            onSuccess: orders => Ok(orders),
            onFailure: Problem
        );
    }

}