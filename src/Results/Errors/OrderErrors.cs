namespace E_Commerce.Results.Errors;

public static class OrderErrors
{
    public static Error OrderNotFound(Guid id) =>
        Error.NotFound(
            code: "ORDER_NOT_FOUND",
            description: $"Order with id '{id}' not found");

    public static Error CartIsEmpty =>
        Error.NotFound(
            code: "ORDER_CART_IS_EMPTY",
            description: "There's no items in the cart to checkout");
}