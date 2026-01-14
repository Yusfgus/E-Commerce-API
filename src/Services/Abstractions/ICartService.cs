using E_Commerce.Requests.Cart;
using E_Commerce.Results;

namespace E_Commerce.Services.Abstractions;

public interface ICartService
{
    Task<Result> AddCartItem(CreateCartItemRequest request, CancellationToken ct);
}