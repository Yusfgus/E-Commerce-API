using E_Commerce.Data.Repositories.Abstractions;
using E_Commerce.Models.Carts;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Repositories.Implementations;

public class CartRepository(AppDbContext context) : ICartRepository
{
    public async Task AddAsync(Cart cart, CancellationToken ct)
    {
        await context.Carts.AddAsync(cart, ct);
    }

    public async Task AddItemAsync(CartItem cartItem, CancellationToken ct)
    {
        await context.CartItems.AddAsync(cartItem, ct);
    }

    public async Task<Cart?> GetByCustomerIdAsync(Guid customerId, CancellationToken ct)
    {
        return await context.Carts.FirstOrDefaultAsync(c => c.CustomerId == customerId, ct);
    }
}