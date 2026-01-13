namespace E_Commerce.Results.Errors;

public static class VendorErrors
{
    public static Error StoreNameRequired =>
        Error.Validation(
            code: "VENDOR_STORE_NAME_REQUIRED",
            description: "Store name is required");

    public static Error StoreNameTooLong =>
        Error.Validation(
            code: "VENDOR_STORE_NAME_TOO_LONG",
            description: "Store name exceeds maximum length");

    public static Error UserIdRequired =>
        Error.Validation(
            code: "VENDOR_USER_ID_REQUIRED",
            description: "UserId is required for vendor");
}
