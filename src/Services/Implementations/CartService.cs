using E_Commerce.Data.Repositories.Abstractions;
using E_Commerce.Dtos;
using E_Commerce.Mappers;
using E_Commerce.Models.Carts;
using E_Commerce.Models.Products;
using E_Commerce.Requests.Cart;
using E_Commerce.Results;
using E_Commerce.Results.Errors;
using E_Commerce.Services.Abstractions;

namespace E_Commerce.Services.Implementations;

public class CartService(IUnitOfWork uow) : ICartService
{
    public async Task<Result<CartItemDto>> AddCartItemAsync(AddCartItemRequest request, Guid customerId, CancellationToken ct)
    {
        Product? product = await uow.ProductRepo.GetByIdAsync(request.ProductId, ct);

        if (product is null)
            return ProductErrors.NotFound(request.ProductId);

        Cart? cart = await uow.CartRepo.GetByCustomerIdAsync(customerId, ct);

        if (cart is null)
            return CartErrors.NotFound;

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

    public async Task<Result> RemoveCartItemAsync(Guid cartItemId, CancellationToken ct)
    {
        CartItem? cartItem = await uow.CartRepo.GetItemAsync(cartItemId, ct);

        if (cartItem is null)
            return CartItemErrors.NotFound(cartItemId);

        uow.CartRepo.RemoveItemAsync(cartItem);

        await uow.SaveChangesAsync(ct);

        return Result.Success;
    }

    public async Task<Result<CartItemDto>> GetCartItemByIdAsync(Guid cartItemId, CancellationToken ct)
    {
        CartItem? cartItem = await uow.CartRepo.GetItemAsync(cartItemId, ct);

        if (cartItem is null)
            return CartItemErrors.NotFound(cartItemId);

        return cartItem.ToDto();
    }

    public async Task<Result<List<CartItemDto>>> GetCartItemsAsync(Guid customerId, CancellationToken ct)
    {
        Cart? cart = await uow.CartRepo.GetByCustomerIdAsync(customerId, ct);

        if (cart is null)
            return CartErrors.NotFound;

        List<CartItem> cartItems = await uow.CartRepo.GetItemsAsync(cart.Id, ct);

        return cartItems.ToDto();
    }
}