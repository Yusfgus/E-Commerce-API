using E_Commerce.Models.Auth;
using E_Commerce.Models.Common;
using E_Commerce.Models.Payments;

namespace E_Commerce.Models.Orders;

public sealed class Order : AuditableEntity
{
    public Guid CustomerId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public decimal TotalPrice => Items.Sum(i => i.Quantity * i.UnitPrice);
    public DateTime CreatedAt { get; set; }

    // Navigation
    public Customer Customer { get; set; } = null!;
    public Payment Payment { get; set; } = null!;
    public ICollection<OrderItem> Items { get; set; } = [];
}
