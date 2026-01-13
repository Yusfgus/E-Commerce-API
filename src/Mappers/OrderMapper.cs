using E_Commerce.Dtos;
using E_Commerce.Models.Orders;

namespace E_Commerce.Mappers;

public static class OrderMapper
{
    public static OrderDto ToDto(this Order order)
    {
        ArgumentNullException.ThrowIfNull(order);

        return new OrderDto
        {
            CustomerId = order.CustomerId,
            OrderStatus = order.OrderStatus,
            TotalPrice = order.TotalPrice,
            CreatedAt = order.CreatedAt,
            Items = order.Items.ToDto()
        };
    }

    public static List<OrderDto> ToDto(this IEnumerable<Order> orders)
    {
        ArgumentNullException.ThrowIfNull(orders);

        return orders.Select(ToDto).ToList();
    }
}
