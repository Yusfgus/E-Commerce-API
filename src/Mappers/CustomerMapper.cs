using E_Commerce.Dtos;
using E_Commerce.Models.Auth;

namespace E_Commerce.Mappers;

public static class CustomerMapper
{
    public static CustomerDto ToDto(this Customer customer)
    {
        return new()
        {
            Id = customer.User.Id,
            Email = customer.User.Email,
            PasswordHash = customer.User.PasswordHash,
            CreatedAtUtc = customer.User.CreatedAtUtc,
            LastModifiedUtc = customer.User.LastModifiedUtc,
            IsActive = customer.User.IsActive,
            PhoneNumber = customer.User.PhoneNumber,
            Role = customer.User.Role,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            ShippingAddress = customer.ShippingAddress,
            BillingAddress = customer.BillingAddress,
        };
    }
}
