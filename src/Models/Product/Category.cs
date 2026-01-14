using E_Commerce.Models.Common;
using E_Commerce.Results;
using E_Commerce.Results.Errors;

namespace E_Commerce.Models.Products;

public sealed class Category : Entity
{
    public string Name { get; private set; } = null!;

    // Navigation
    public ICollection<Product> Products { get; private set; }  = [];

    private Category() {} // EF Core

    private Category(string name)
    {
        Name = name;
    }

    public static Result<Category> Create(string name)
    {
        List<Error> errors = [];

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(CategoryErrors.NameRequired);

        if (name.Length < 3)
            errors.Add(CategoryErrors.NameTooShort);

        if (name.Length > 50)
            errors.Add(CategoryErrors.NameTooLong);

        if (errors.Count != 0)
        {
            return errors;
        }

        Category category = new(name);

        return category;
    }

    public Result UpdateName(string name)
    {
        List<Error> errors = [];

        if (name.Length < 3)
            errors.Add(CategoryErrors.NameTooShort);

        if (name.Length > 50)
            errors.Add(CategoryErrors.NameTooLong);

        if (errors.Count != 0)
        {
            return errors;
        }

        Name = name;

        return Result.Success;
    }
}