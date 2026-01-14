using E_Commerce.Dtos;
using E_Commerce.Models.Auth;

namespace E_Commerce.Mappers;

public static class AdminMapper
{
    public static AdminDto ToDto(this Admin admin)
    {
        return new()
        {
            Id = admin.User.Id,
            Email = admin.User.Email,
            PasswordHash = admin.User.PasswordHash,
            CreatedAtUtc = admin.User.CreatedAtUtc,
            LastModifiedUtc = admin.User.LastModifiedUtc,
            IsActive = admin.User.IsActive,
            PhoneNumber = admin.User.PhoneNumber,
            Role = admin.User.Role,
            LastActionAtUtc = admin.LastActionAtUtc
        };
    }
}