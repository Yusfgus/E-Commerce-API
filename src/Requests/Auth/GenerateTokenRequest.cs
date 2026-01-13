using E_Commerce.Models.Auth;

namespace E_Commerce.Requests.Auth;

public class GenerateTokenRequest
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = null!;
    public UserRole Role { get; set; }
}
