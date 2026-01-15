using E_Commerce.Data.Repositories.Abstractions;
using E_Commerce.Dtos;
using E_Commerce.Mappers;
using E_Commerce.Models.Carts;
using E_Commerce.Requests.Cart;
using E_Commerce.Results;
using E_Commerce.Results.Errors;
using E_Commerce.Services.Abstractions;

namespace E_Commerce.Services.Implementations;

public class CartService(ICartRepository cartRepo,
                         IProductRepository productRepo,
                         IUnitOfWork uow) 
    : ICartService
{
    public async Task<Result<CartItemDto>> AddCartItem(AddCartItemRequest request, Guid customerId, CancellationToken ct)
    {
        if (!await productRepo.IsExistAsync(request.ProductId, ct))
            return ProductErrors.NotFound(request.ProductId);

        var result = await CreateCartIfNotExistsAsync(customerId, ct);

        if (result.IsFailure)
        {
            return result.Errors;
        }

        var cartItemResult = CartItem.Create(
            cartId: result.Value!,
            productId: request.ProductId,
            quantity: request.Quantity
        );

        if (cartItemResult.IsFailure)
        {
            return cartItemResult.Errors;
        }

        CartItem cartItem = cartItemResult.Value!;

        await cartRepo.AddItemAsync(cartItem, ct);

        await uow.SaveChangesAsync(ct);

        return cartItem.ToDto();
    }

    public async Task<Result> CreateCartAsync(Guid customerId, CancellationToken ct)
    {
        Cart? cart = await cartRepo.GetByCustomerIdAsync(customerId, ct);
        
        if (cart is null)
        {
            var cartResult = Cart.Create(customerId);

            if (cartResult.IsFailure)
            {
                return cartResult.Errors;
            }

            cart = cartResult.Value!;

            await cartRepo.AddAsync(cart, ct);

            await uow.SaveChangesAsync(ct);
        }

        return Result.Success;
    }

}