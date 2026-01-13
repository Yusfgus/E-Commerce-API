namespace E_Commerce.Results.Errors;

public static class AdminErrors
{
    public static Error UserIdRequired =>
        Error.Validation(
            code: "ADMIN_USER_ID_REQUIRED",
            description: "UserId is required for admin");

    public static Error LastActionInFuture =>
        Error.Validation(
            code: "ADMIN_LAST_ACTION_IN_FUTURE",
            description: "Last action date cannot be in the future");
}
