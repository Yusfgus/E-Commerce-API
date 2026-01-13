using E_Commerce.Data;using E_Commerce.Data.Repositories.Abstractions;
using E_Commerce.Dtos;
using E_Commerce.Models.Orders;
using E_Commerce.Results;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Repositories.Implementations;

public class OrderRepository(AppDbContext context) : IOrderRepository
{
    public async Task<int> CountAsync(Guid customerId, CancellationToken ct)
    {
        return await context.Orders
                    .Where(o => o.CustomerId == customerId)
                    .CountAsync(ct);
    }

    public async Task<List<Order>> GetByCustomerIdAsync(Guid customerId, int page, int pageSize, CancellationToken ct)
    {
        return await context.Orders
                    .Where(o => o.CustomerId == customerId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(ct);
    }

    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await context.Orders.FirstOrDefaultAsync(o => o.Id == id, ct);
    }
}