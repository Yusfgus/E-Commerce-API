using E_Commerce.Dtos;
using E_Commerce.Models.Auth;

namespace E_Commerce.Mappers;

public static class UserMapper
{
    public static UserDto ToDto(this User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            CreatedAtUtc = user.CreatedAtUtc,
            LastModifiedUtc = user.LastModifiedUtc,
            LastLoginAtUtc = user.LastLoginAtUtc,
            IsActive = user.IsActive,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role,
        };
    }

    public static List<UserDto> ToDto(this IEnumerable<User> users)
    {
        ArgumentNullException.ThrowIfNull(users);

        return users.Select(ToDto).ToList();
    }
}
