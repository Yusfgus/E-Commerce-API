using E_Commerce.Models.Carts;

namespace E_Commerce.Data.Repositories.Abstractions;

public interface ICartRepository
{
    Task<Cart?> GetByCustomerIdAsync(Guid customerId, CancellationToken ct);
    Task AddItemAsync(CartItem cartItem, CancellationToken ct);
    Task AddAsync(Cart cart, CancellationToken ct);
}