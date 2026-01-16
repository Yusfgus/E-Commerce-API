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

    public async Task<CartItem?> GetItemAsTrackingAsync(Guid customerId, Guid cartItemId, CancellationToken ct)
    {
        return await context.CartItems.AsTracking()
                            .Include(ci => ci.Cart)
                            .FirstOrDefaultAsync(ci => 
                                ci.Id == cartItemId &&
                                ci.Cart.Customer.UserId == customerId &&
                                ci.Cart.Status == CartStatus.Active, ct
                            );
    }

    public async Task<CartItem?> GetItemAsync(Guid customerId, Guid cartItemId, CancellationToken ct)
    {
        return await context.CartItems
                            .Include(ci => ci.Cart)
                            .FirstOrDefaultAsync(ci => 
                                ci.Id == cartItemId &&
                                ci.Cart.Customer.UserId == customerId &&
                                ci.Cart.Status == CartStatus.Active, ct
                            );
    }

    public async Task<List<CartItem>> GetItemsAsync(Guid customerId, CancellationToken ct)
    {
        return await context.CartItems
                            .Include(ci => ci.Cart)
                            .Where(ci => 
                                ci.Cart.Customer.UserId == customerId &&
                                ci.Cart.Status == CartStatus.Active
                            )
                            .ToListAsync(ct);
    }

    public async Task<bool> IsItemExistAsync(Guid productId, Guid cartId, CancellationToken ct)
    {
        return await context.CartItems.AnyAsync(ci => ci.CartId == cartId && ci.ProductId == productId, ct);
    }

    public async Task<bool> IsItemExistAsync(Guid cartItemId, CancellationToken ct)
    {
        return await context.CartItems.AnyAsync(ci => ci.Id == cartItemId, ct);
    }

    public void RemoveItemAsync(CartItem cartItem)
    {
        context.CartItems.Remove(cartItem);
    }
}