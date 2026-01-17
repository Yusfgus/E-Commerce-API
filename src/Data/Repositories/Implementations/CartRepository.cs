using System.Linq.Expressions;
using E_Commerce.Data.Repositories.Abstractions;
using E_Commerce.Models.Carts;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Repositories.Implementations;

public class CartRepository(AppDbContext context) : ICartRepository
{
    public async Task CreateAsync(Cart cart, CancellationToken ct)
    {
        await context.Carts.AddAsync(cart, ct);
    }

    public async Task<Cart?> GetByCustomerIdAsTrackingAsync(Guid customerId, Expression<Func<CartItem, bool>>? includeItemsFilter, CancellationToken ct)
    {
        var query = context.Carts.AsTracking()
            .Where(c => c.CustomerId == customerId && c.Status == CartStatus.Active);

        if (includeItemsFilter != null)
        {
            query = query.Include(c => c.Items.AsQueryable().Where(includeItemsFilter));
        }

        return await query.FirstOrDefaultAsync(ct);
    }

}