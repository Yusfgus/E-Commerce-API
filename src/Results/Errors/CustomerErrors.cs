namespace E_Commerce.Results.Errors;

public static class CustomerErrors
{
    public static Error UserIdRequired =>
        Error.Validation(
            code: "CUSTOMER_USER_ID_REQUIRED",
            description: "User id is required");

    public static Error FirstNameRequired =>
        Error.Validation(
            code: "CUSTOMER_FIRST_NAME_REQUIRED",
            description: "First name is required");

    public static Error LastNameRequired =>
        Error.Validation(
            code: "CUSTOMER_LAST_NAME_REQUIRED",
            description: "Last name is required");

    public static Error InvalidShippingAddress =>
        Error.Validation(
            code: "CUSTOMER_INVALID_SHIPPING_ADDRESS",
            description: "Shipping address is invalid");

    public static Error InvalidBillingAddress =>
        Error.Validation(
            code: "CUSTOMER_INVALID_BILLING_ADDRESS",
            description: "Billing address is invalid");
}
