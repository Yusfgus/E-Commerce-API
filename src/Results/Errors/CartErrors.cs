namespace E_Commerce.Results.Errors;

public static class CartErrors
{
    public static Error InvalidCustomerId =>
        Error.Validation(
            code: "CART_INVALID_CUSTOMER_ID",
            description: "Customer id is invalid");

    public static Error CartNotActive =>
        Error.Conflict(
            code: "CART_NOT_ACTIVE",
            description: "Cart is not active");

    public static Error CartEmpty =>
        Error.Conflict(
            code: "CART_EMPTY",
            description: "Cannot checkout an empty cart");

    public static Error ItemNotFound =>
        Error.NotFound(
            code: "CART_ITEM_NOT_FOUND",
            description: "Cart item not found");
}

