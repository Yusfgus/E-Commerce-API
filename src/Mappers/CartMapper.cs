using E_Commerce.Dtos;
using E_Commerce.Models.Carts;

namespace E_Commerce.Mappers;

public static class CartMapper
{
    public static CartDto ToDto(this Cart cart)
    {
        ArgumentNullException.ThrowIfNull(cart);

        return new CartDto
        {
            CustomerId = cart.CustomerId,
            Status = cart.Status,
            Items = cart.Items.ToDto()
        };
    }

    public static List<CartDto> ToDto(this ICollection<Cart> carts)
    {
        ArgumentNullException.ThrowIfNull(carts);

        return carts.Select(ToDto).ToList();
    }
}
