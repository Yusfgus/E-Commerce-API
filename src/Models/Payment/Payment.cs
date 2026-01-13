using E_Commerce.Models.Auth;
using E_Commerce.Models.Common;
using E_Commerce.Models.Orders;

namespace E_Commerce.Models.Payments;

public sealed class Payment : AuditableEntity
{
    public Guid CustomerId { get; set; }
    public Guid OrderId { get; set; }
    public Guid PaymentMethodId { get; set; }
    
    public decimal Amount { get; set; }
    public PaymentStatus PaymentStatus { get; set; }

    // Navigation
    public Customer Customer { get; set; } = null!;
    public Order Order { get; set; } = null!;
    public PaymentMethod PaymentMethod { get; set; } = null!;
}
