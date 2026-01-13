using E_Commerce.Models.Carts;

namespace E_Commerce.Dtos;

public sealed class CartDto : EntityDto
{
    public Guid CustomerId { get; set; }
    public CartStatus Status { get; set; }
    public List<CartItemDto>? Items { get; set; }
}
