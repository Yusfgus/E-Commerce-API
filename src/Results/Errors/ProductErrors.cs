namespace E_Commerce.Results.Errors;

public static class ProductErrors
{
    public static Error VendorIdRequired =>
        Error.Validation(
            code: "PRODUCT_VENDOR_ID_REQUIRED",
            description: "Vendor id is required");

    public static Error NameRequired =>
        Error.Validation(
            code: "PRODUCT_NAME_REQUIRED",
            description: "Product name is required");

    public static Error NameTooLong =>
        Error.Validation(
            code: "PRODUCT_NAME_TOO_LONG",
            description: "Product name exceeds the maximum allowed length");

    public static Error DescriptionRequired =>
        Error.Validation(
            code: "PRODUCT_DESCRIPTION_REQUIRED",
            description: "Product description is required");

    public static Error InvalidPrice =>
        Error.Validation(
            code: "PRODUCT_INVALID_PRICE",
            description: "Product price must be greater than zero");

    public static Error InvalidStockQuantity =>
        Error.Validation(
            code: "PRODUCT_INVALID_STOCK_QUANTITY",
            description: "Stock quantity cannot be negative");

    public static Error CategoryIdRequired =>
        Error.Validation(
            code: "PRODUCT_CATEGORY_ID_REQUIRED",
            description: "Category id is required");    

// ---------

    public static Error NotFound(Guid id) =>
        Error.NotFound(
            code: "PRODUCT_NOT_FOUND",
            description: $"Product with id '{id}' not found");

    public static Error VendorNotFound(Guid vendorId) =>
        Error.NotFound(
            code: "PRODUCT_VENDOR_NOT_FOUND",
            description: $"Vendor with id '{vendorId}' not found");

    public static Error NotFound(string name) =>
        Error.NotFound(
            code: "PRODUCT_NOT_FOUND",
            description: $"Product with name '{name}' not found");

    public static Error NameInUse(string name) =>
        Error.Conflict(
            code: "PRODUCT_NAME_TAKEN",
            description: $"Product name '{name}' is already in use");

    public static Error ProductAccessDenied =>
        Error.Forbidden(
            code: "PRODUCT_ACCESS_DENIED",
            description: "You are not allowed to modify this product");

}
