namespace E_Commerce.Results.Errors;

public static class OrderErrors
{
    public static Error OrderNotFound(Guid id) =>
        Error.NotFound(
            code: "ORDER_NOT_FOUND",
            description: $"Order with id '{id}' not found");
}