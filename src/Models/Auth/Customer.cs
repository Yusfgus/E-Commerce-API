using E_Commerce.Results.Errors;
using E_Commerce.Models.Carts;
using E_Commerce.Models.Orders;
using E_Commerce.Models.Payments;
using E_Commerce.Results;

namespace E_Commerce.Models.Auth;

public sealed class Customer
{
    public Guid UserId { get; private set; }
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string FullName => $"{FirstName} {LastName}";
    public string? ShippingAddress { get; private set; }
    public string? BillingAddress { get; private set; }

    // Navigation
    public User User { get; private set; } = null!;
    public Cart? Cart { get; private set; }
    public ICollection<Order> Orders { get; private set; } = [];
    public ICollection<Payment> Payments { get; private set; } = [];

    private Customer() { } // EF Core

    private Customer(Guid userId, string firstName, string lastName, string? shippingAddress, string? billingAddress)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
    }

    public static Result<Customer> Create(Guid userId, string firstName, string lastName, string? shippingAddress, string? billingAddress)
    {
        List<Error> errors = [];

        if (userId == Guid.Empty)
            errors.Add(CustomerErrors.UserIdRequired);

        if (string.IsNullOrWhiteSpace(firstName))
            errors.Add(CustomerErrors.FirstNameRequired);

        if (string.IsNullOrWhiteSpace(lastName))
            errors.Add(CustomerErrors.LastNameRequired);

        if (errors.Count != 0)
            return errors;

        var customer = new Customer(
            userId,
            firstName.Trim(),
            lastName.Trim(),
            shippingAddress?.Trim(),
            billingAddress?.Trim());

        return customer;
    }

    public Result<Updated> UpdateFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return CustomerErrors.FirstNameRequired;

        FirstName = firstName.Trim();
        return Result.Updated;
    }

    public Result<Updated> UpdateLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            return CustomerErrors.LastNameRequired;

        LastName = lastName.Trim();
        return Result.Updated;
    }

    public Result<Updated> UpdateShippingAddress(string? shippingAddress)
    {
        ShippingAddress = shippingAddress?.Trim();
        return Result.Updated;
    }

    public Result<Updated> UpdateBillingAddress(string? billingAddress)
    {
        BillingAddress = billingAddress?.Trim();
        return Result.Updated;
    }
}
