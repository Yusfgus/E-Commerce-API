using System.Linq.Expressions;
using E_Commerce.Models.Carts;

namespace E_Commerce.Data.Repositories.Abstractions;

public interface ICartRepository
{
    Task CreateAsync(Cart cart, CancellationToken ct);
    Task<Cart?> GetByCustomerIdAsTrackingAsync(Guid customerId, Expression<Func<CartItem, bool>>? includeItemsFilter, CancellationToken ct);
}