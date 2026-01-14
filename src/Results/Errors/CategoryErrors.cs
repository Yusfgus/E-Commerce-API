namespace E_Commerce.Results.Errors;

public static class CategoryErrors
{
    public static Error NameRequired =>
        Error.Validation(
            code: "CATEGORY_NAME_REQUIRED",
            description: "Category name is required");

    public static Error NameTooLong =>
        Error.Validation(
            code: "CATEGORY_NAME_TOO_LONG",
            description: "Category name exceeds maximum length");

    public static Error NameInvalid =>
        Error.Validation(
            code: "CATEGORY_NAME_INVALID",
            description: "Category name contains invalid characters");
            
// ---------

    public static Error NotFound(string category) =>
        Error.NotFound(
            code: "CATEGORY_NOT_FOUND",
            description: $"Category '{category}' does not exist");

    public static Error NameTaken =>
        Error.Conflict(
            code: "CATEGORY_NAME_TAKEN",
            description: "Category name already exists");

    public static Error HasProducts =>
        Error.Conflict(
            code: "CATEGORY_HAS_PRODUCTS",
            description: "Cannot delete category with existing products");
}
