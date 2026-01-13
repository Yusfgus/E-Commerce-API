using E_Commerce.Dtos;
using E_Commerce.Models.Orders;

namespace E_Commerce.Mappers;

public static class OrderItemMapper
{
    public static OrderItemDto ToDto(this OrderItem orderItem)
    {
        ArgumentNullException.ThrowIfNull(orderItem);

        return new OrderItemDto
        {
            OrderId = orderItem.OrderId,
            ProductId = orderItem.ProductId,
            Quantity = orderItem.Quantity,
            UnitPrice = orderItem.UnitPrice,
            Product = orderItem.Product.ToDto() 
        };
    }

    public static List<OrderItemDto> ToDto(this IEnumerable<OrderItem> orderItems)
    {
        ArgumentNullException.ThrowIfNull(orderItems);

        return orderItems.Select(ToDto).ToList();
    }
}
