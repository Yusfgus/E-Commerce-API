namespace E_Commerce.Results.Errors;

public static class CartItemErrors
{
    public static Error InvalidCartId =>
        Error.Validation(
            code: "CART_ITEM_INVALID_CART_ID",
            description: "Cart id is invalid");

    public static Error InvalidProductId =>
        Error.Validation(
            code: "CART_ITEM_INVALID_PRODUCT_ID",
            description: "Product id is invalid");

    public static Error InvalidQuantity =>
        Error.Validation(
            code: "CART_ITEM_INVALID_QUANTITY",
            description: "Quantity must be greater than zero");

    public static Error QuantityBelowOne =>
        Error.Conflict(
            code: "CART_ITEM_QUANTITY_BELOW_ONE",
            description: "Cart item quantity cannot be less than one");

// -----

    public static Error NotFound(Guid id) =>
        Error.Conflict(
            code: "CART_ITEM_NOT_FOUND",
            description: $"Cart item with id '{id}' not found");

}

