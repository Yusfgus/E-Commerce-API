using E_Commerce.Models.Carts;

namespace E_Commerce.Data.Repositories.Abstractions;

public interface ICartRepository
{
    Task<Cart?> GetByCustomerIdAsync(Guid customerId, CancellationToken ct);
    Task AddItemAsync(CartItem cartItem, CancellationToken ct);
    Task AddAsync(Cart cart, CancellationToken ct);
    Task<bool> IsItemExistAsync(Guid productId, Guid cartId, CancellationToken ct);
    Task<List<CartItem>> GetItemsAsync(Guid cartId, CancellationToken ct);
    Task<CartItem?> GetItemAsync(Guid cartItemId, CancellationToken ct);
    Task<bool> IsItemExistAsync(Guid cartItemId, CancellationToken ct);
    void RemoveItemAsync(CartItem cartItem);
}