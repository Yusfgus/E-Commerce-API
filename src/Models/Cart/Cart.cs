using E_Commerce.Models.Auth;
using E_Commerce.Models.Common;
using E_Commerce.Results;
using E_Commerce.Results.Errors;

namespace E_Commerce.Models.Carts;

public sealed class Cart : AuditableEntity
{
    public Guid CustomerId { get; private set; }
    public CartStatus Status { get; private set; }

    // Navigation
    public Customer Customer { get; private set; } = null!;
    public ICollection<CartItem> Items { get; private set; } = new List<CartItem>();

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

        Items.Add(item);
        return Result.Success;
    }

    public Result RemoveItem(Guid productId)
    {
        if (Status != CartStatus.Active)
            return CartErrors.CartNotActive;

        var item = Items.FirstOrDefault(i => i.ProductId == productId);
        if (item is null)
            return CartErrors.ItemNotFound;

        Items.Remove(item);
        return Result.Success;
    }

    public Result Checkout()
    {
        if (Status != CartStatus.Active)
            return CartErrors.CartNotActive;

        if (!Items.Any())
            return CartErrors.CartEmpty;

        Status = CartStatus.CheckedOut;
        return Result.Success;
    }
}

