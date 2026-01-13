using E_Commerce.Dtos;
using E_Commerce.Models.Payments;

namespace E_Commerce.Mappers;

public static class PaymentMapper
{
    public static PaymentDto ToDto(this Payment payment)
    {
        ArgumentNullException.ThrowIfNull(payment);

        return new PaymentDto
        {
            UserId = payment.CustomerId,
            OrderId = payment.OrderId,
            Amount = payment.Amount,
            PaymentStatus = payment.PaymentStatus,
            PaymentMethod = payment.PaymentMethod.ToDto()
        };
    }

    public static List<PaymentDto> ToDtos(this IEnumerable<Payment> payments)
    {
        ArgumentNullException.ThrowIfNull(payments);
        
        return payments.Select(ToDto).ToList();
    }
}
