namespace E_Commerce.Results;

public readonly record struct Error
{
    public string Code { get; }
    public string Description { get; }
    public ErrorType Type { get; }

    private Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    public static Error Create(ErrorType type, string code, string description)
        => new(code, description, type);

// --------

    public static Error Validation(string code = nameof(Validation), string description = "Validation error")
        => new(code, description, ErrorType.Validation);

    public static Error NotFound(string code = nameof(NotFound), string description = "Not found error")
        => new(code, description, ErrorType.NotFound);

    public static Error Conflict(string code = nameof(Conflict), string description = "Conflict error")
        => new(code, description, ErrorType.Conflict);

    public static Error Unauthorized(string code = nameof(Unauthorized), string description = "Unauthorized error")
        => new(code, description, ErrorType.Unauthorized);

    public static Error Forbidden(string code = nameof(Forbidden), string description = "Forbidden error")
        => new(code, description, ErrorType.Forbidden);

    public static Error BusinessRule(string code = nameof(BusinessRule), string description = "Business rule error")
        => new(code, description, ErrorType.BusinessRule);

    public static Error Unexpected(string code = nameof(Unexpected), string description = "Unexpected error.")
        => new(code, description, ErrorType.Unexpected);

}