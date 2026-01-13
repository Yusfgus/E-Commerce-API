namespace E_Commerce.Request.Auth;

public abstract class RegisterUserRequest
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? PhoneNumber { get; set; }
}
