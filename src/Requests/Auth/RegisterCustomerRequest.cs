using E_Commerce.Models.Auth;

namespace E_Commerce.Request.Auth;

public class RegisterCustomerRequest : RegisterUserRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ShippingAddress { get; set; }
    public string? BillingAddress { get; set; }
}
