namespace E_Commerce.Dtos;

public class CustomerDto : UserDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ShippingAddress { get; set; }
    public string? BillingAddress { get; set; }
}
