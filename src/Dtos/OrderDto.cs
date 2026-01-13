using E_Commerce.Models.Orders;

namespace E_Commerce.Dtos;

public sealed class OrderDto : EntityDto
{
    public Guid CustomerId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<OrderItemDto>? Items { get; set; }
}
