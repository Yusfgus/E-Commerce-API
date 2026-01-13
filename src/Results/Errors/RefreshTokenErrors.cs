namespace E_Commerce.Results.Errors;

public static class RefreshTokenErrors
{
    public static Error UserIdRequired =>
        Error.Validation(
            code: "REFRESH_TOKEN_USER_ID_REQUIRED",
            description: "UserId is required for refresh token");

    public static Error TokenRequired =>
        Error.Validation(
            code: "REFRESH_TOKEN_VALUE_REQUIRED",
            description: "Refresh token value is required");

    public static Error ExpiryInPast =>
        Error.Validation(
            code: "REFRESH_TOKEN_EXPIRY_IN_PAST",
            description: "Refresh token expiry must be in the future");

// -----

    public static Error Invalid =>
        Error.Unauthorized(
            code: "REFRESH_TOKEN_INVALID",
            description: "Invalid refresh token");

    public static Error Expired(DateTime expiryDate) =>
        Error.Unauthorized(
            code: "REFRESH_TOKEN_EXPIRED",
            description: $"RefreshToken was expired since {expiryDate}");
}
