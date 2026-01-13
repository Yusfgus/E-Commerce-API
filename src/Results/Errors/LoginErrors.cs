namespace E_Commerce.Results.Errors;

public static class LoginErrors
{
    public static Error LoginFailed =>
        Error.Unauthorized(
            code: "LOGIN_FAILED",
            description: "Invalid email or password");

    public static Error LoginInvalidCredentials =>
        Error.Unauthorized(
            code: "LOGIN_INVALID_CREDENTIALS",
            description: "Invalid credentials");

    public static Error LoginAccountInActive =>
        Error.Unauthorized(
            code: "LOGIN_ACCOUNT_INACTIVE",
            description: "This account is not active");

}