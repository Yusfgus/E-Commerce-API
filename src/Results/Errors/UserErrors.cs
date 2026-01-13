namespace E_Commerce.Results.Errors;

public static class UserErrors
{
    public static Error EmailRequired =>
        Error.Validation(
            code: "USER_EMAIL_REQUIRED",
            description: "Email is required");

    public static Error EmailInvalid =>
        Error.Validation(
            code: "USER_INVALID_EMAIL",
            description: "Email format is invalid");

    public static Error PasswordHashRequired =>
        Error.Validation(
            code: "USER_PASSWORD_HASH_REQUIRED",
            description: "Password hash is required");

    public static Error UserInactive =>
        Error.BusinessRule(
            code: "USER_INACTIVE",
            description: "User account is inactive");

    public static Error PhoneNumberInvalid =>
        Error.Validation(
            code: "USER_INVALID_PHONE_NUMBER",
            description: "Phone number format is invalid");

// -----
    
    public static Error NotFound(Guid id) =>
        Error.NotFound(
            code: "USER_NOT_FOUND",
            description: $"User with id '{id}' not found");

    public static Error EmailInUse(string email) =>
        Error.Conflict(
            code: "EMAIL_TAKEN",
            description: $"Email '{email}' is already in use");

    public static Error PhoneNumberInUse(string phoneNumber) =>
        Error.Conflict(
            code: "PHONE_NUMBER_TAKEN",
            description: $"Phone number '{phoneNumber}' is already in use");
}
