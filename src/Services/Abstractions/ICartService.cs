using E_Commerce.Dtos;
using E_Commerce.Requests.Cart;
using E_Commerce.Results;

namespace E_Commerce.Services.Abstractions;

public interface ICartService
{
    Task<Result<CartItemDto>> AddCartItemAsync(AddCartItemRequest request, Guid customerId, CancellationToken ct);
    Task<Result> RemoveCartItemAsync(Guid customerId, Guid cartItemId, CancellationToken ct);
    Task<Result<CartItemDto>> GetCartItemByIdAsync(Guid customerId, Guid cartItemId, CancellationToken ct);
    Task<Result<List<CartItemDto>>> GetCartItemsAsync(Guid customerId, CancellationToken ct);
    Task<Result> UpdateCartItemQuantityAsync(Guid customerId, Guid cartItemId, UpdateCartItemQuantityRequest request, CancellationToken ct);
}