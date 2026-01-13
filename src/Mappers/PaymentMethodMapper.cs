using E_Commerce.Dtos;
using E_Commerce.Models.Payments;

namespace E_Commerce.Mappers;

public static class PaymentMethodMapper
{
    public static PaymentMethodDto ToDto(this PaymentMethod paymentMethod)
    {
        ArgumentNullException.ThrowIfNull(paymentMethod);

        return new PaymentMethodDto
        {
            Name = paymentMethod.Name,
        };
    }

    public static List<PaymentMethodDto> ToDto(this IEnumerable<PaymentMethod> paymentMethods)
    {
        ArgumentNullException.ThrowIfNull(paymentMethods);
        
        return paymentMethods.Select(ToDto).ToList();
    }
}
