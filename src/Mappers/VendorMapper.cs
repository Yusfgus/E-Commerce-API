using E_Commerce.Dtos;
using E_Commerce.Models.Auth;

namespace E_Commerce.Mappers;

public static class VendorMapper
{
    public static VendorDto ToDto(this Vendor vendor)
    {
        return new()
        {
            Id = vendor.User.Id,
            Email = vendor.User.Email,
            PasswordHash = vendor.User.PasswordHash,
            CreatedAtUtc = vendor.User.CreatedAtUtc,
            LastModifiedUtc = vendor.User.LastModifiedUtc,
            IsActive = vendor.User.IsActive,
            PhoneNumber = vendor.User.PhoneNumber,
            Role = vendor.User.Role,
            StoreName = vendor.StoreName,
        };
    }
}