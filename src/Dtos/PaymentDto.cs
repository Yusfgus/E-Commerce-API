using E_Commerce.Models.Payments;

namespace E_Commerce.Dtos;

public sealed class PaymentDto
{
    public Guid UserId { get; set; }
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public PaymentMethodDto? PaymentMethod { get; set; }
}
