using E_Commerce.Dtos;
using E_Commerce.Requests.Cart;
using E_Commerce.Results;

namespace E_Commerce.Services.Abstractions;

public interface ICartService
{
    Task<Result<CartItemDto>> AddCartItem(AddCartItemRequest request, Guid customerId, CancellationToken ct);
    Task<Result> CreateCartAsync(Guid customerId, CancellationToken ct);

}