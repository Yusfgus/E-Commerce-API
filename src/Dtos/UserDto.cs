using E_Commerce.Models.Auth;

namespace E_Commerce.Dtos;

public class UserDto : EntityDto
{
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public DateTimeOffset? CreatedAtUtc { get; set; }
    public DateTimeOffset? LastModifiedUtc { get; set; }
    public DateTimeOffset? LastLoginAtUtc { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public UserRole Role { get; set; }
}
