using E_Commerce.Dtos;
using E_Commerce.Models.Carts;

namespace E_Commerce.Mappers;

public static class CartItemMapper
{
    public static CartItemDto ToDto(this CartItem cartItem)
    {
        ArgumentNullException.ThrowIfNull(cartItem);

        return new CartItemDto
        {
            CartId = cartItem.CartId,
            ProductId = cartItem.ProductId,
            Quantity = cartItem.Quantity,
            Product = cartItem.Product.ToDto()
        };
    }

    public static List<CartItemDto> ToDto(this ICollection<CartItem> cartItems)
    {
        ArgumentNullException.ThrowIfNull(cartItems);

        return cartItems.Select(ToDto).ToList();
    }
}