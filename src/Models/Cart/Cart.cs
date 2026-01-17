using E_Commerce.Models.Auth;
using E_Commerce.Models.Common;
using E_Commerce.Results;
using E_Commerce.Results.Errors;

namespace E_Commerce.Models.Carts;

public sealed class Cart : AuditableEntity
{
    public Guid CustomerId { get; private set; }
    public CartStatus Status { get; private set; }
    // public DateTimeOffset? CheckedOutAt { get; private set; }

    // Navigation
    public Customer Customer { get; private set; } = null!;
    public ICollection<CartItem> Items { get; private set; } = [];

    private Cart(Guid customerId)
    {
        CustomerId = customerId;
        Status = CartStatus.Active;
    }

    public static Result<Cart> Create(Guid customerId)
    {
        if (customerId == Guid.Empty)
            return CartErrors.InvalidCustomerId;

        return new Cart(customerId);
    }

    // ---------------- Domain behaviors ----------------

    public Result AddItem(CartItem item)
    {
        if (Status != CartStatus.Active)
            return CartErrors.CartNotActive;

        if (Items.Any(i => i.ProductId == item.ProductId))
            return CartErrors.ItemAlreadyAdded;

        Items.Add(item);

        return Result.Success;
    }

    public Result RemoveItem(Guid itemId)
    {
        if (Status != CartStatus.Active)
            return CartErrors.CartNotActive;

        CartItem? item = Items.FirstOrDefault(i => i.Id == itemId);
        if (item is null)
            return CartErrors.ItemNotFound;

        Items.Remove(item);

        return Result.Success;
    }

    public Result Checkout()
    {
        if (Status != CartStatus.Active)
            return CartErrors.CartNotActive;

        if (Items.Count == 0)
            return CartErrors.CartEmpty;

        Status = CartStatus.CheckedOut;
        // CheckedOutAt = DateTimeOffset.UtcNow;

        return Result.Success;
    }
}

