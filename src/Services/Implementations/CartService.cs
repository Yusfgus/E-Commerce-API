using E_Commerce.Data.Repositories.Abstractions;
using E_Commerce.Dtos;
using E_Commerce.Mappers;
using E_Commerce.Models.Carts;
using E_Commerce.Requests.Cart;
using E_Commerce.Results;
using E_Commerce.Results.Errors;
using E_Commerce.Services.Abstractions;

namespace E_Commerce.Services.Implementations;

public class CartService(IUnitOfWork uow) : ICartService
{
    public async Task<Result<CartItemDto>> AddCartItem(AddCartItemRequest request, Guid customerId, CancellationToken ct)
    {
        if (!await uow.ProductRepo.IsExistAsync(request.ProductId, ct))
            return ProductErrors.NotFound(request.ProductId);

        Cart? cart = await uow.CartRepo.GetByCustomerIdAsync(customerId, ct);

        if (cart is null)
            return CartErrors.NotFound(customerId);

        if (await uow.CartRepo.IsItemExistAsync(productId: request.ProductId, cartId: cart.Id, ct))
            return CartErrors.ItemAlreadyAdded;

        var cartItemResult = CartItem.Create(
            cartId: cart.Id,
            productId: request.ProductId,
            quantity: request.Quantity
        );

        if (cartItemResult.IsFailure)
        {
            return cartItemResult.Errors;
        }

        CartItem cartItem = cartItemResult.Value!;

        await uow.CartRepo.AddItemAsync(cartItem, ct);

        await uow.SaveChangesAsync(ct);

        return cartItem.ToDto();
    }

    public async Task<Result> CreateCartAsync(Guid customerId, CancellationToken ct)
    {
        var cartResult = Cart.Create(customerId);

        if (cartResult.IsFailure)
        {
            return cartResult.Errors;
        }

        Cart cart = cartResult.Value!;

        await uow.CartRepo.AddAsync(cart, ct);

        await uow.SaveChangesAsync(ct);

        return Result.Success;
    }

}