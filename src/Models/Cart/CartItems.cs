using E_Commerce.Models.Products;
using E_Commerce.Results;
using E_Commerce.Results.Errors;

namespace E_Commerce.Models.Carts;

public sealed class CartItem
{
    public Guid CartId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }

    // Navigation
    public Cart Cart { get; private set; } = null!;
    public Product Product { get; private set; } = null!;

    private CartItem(Guid cartId, Guid productId, int quantity)
    {
        CartId = cartId;
        ProductId = productId;
        Quantity = quantity;
    }

    public static Result<CartItem> Create(
        Guid cartId,
        Guid productId,
        int quantity)
    {
        if (cartId == Guid.Empty)
            return CartItemErrors.InvalidCartId;

        if (productId == Guid.Empty)
            return CartItemErrors.InvalidProductId;

        if (quantity <= 0)
            return CartItemErrors.InvalidQuantity;

        return new CartItem(cartId, productId, quantity);
    }

    // ---------------- Domain behaviors ----------------

    public Result IncreaseQuantity(int amount)
    {
        if (amount <= 0)
            return CartItemErrors.InvalidQuantity;

        Quantity += amount;
        return Result.Success;
    }

    public Result DecreaseQuantity(int amount)
    {
        if (amount <= 0)
            return CartItemErrors.InvalidQuantity;

        if (Quantity - amount <= 0)
            return CartItemErrors.QuantityBelowOne;

        Quantity -= amount;
        return Result.Success;
    }

    public Result SetQuantity(int quantity)
    {
        if (quantity <= 0)
            return CartItemErrors.InvalidQuantity;

        Quantity = quantity;
        return Result.Success;
    }
}
