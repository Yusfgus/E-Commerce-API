namespace E_Commerce.Request.Auth;

public class RegisterVendorRequest : RegisterUserRequest
{
    public string? StoreName { get; set; }
}
