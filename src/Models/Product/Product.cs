using E_Commerce.Results.Errors;
using E_Commerce.Models.Auth;
using E_Commerce.Models.Common;
using E_Commerce.Results;

namespace E_Commerce.Models.Products;

public sealed class Product : AuditableEntity
{
    public Guid VendorId { get; private set; }
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }
    public Guid CategoryId { get; private set; }

    // Navigation
    public Vendor Vendor { get; private set; } = null!;
    public Category Category { get; private set; } = null!;

    private Product() { } // EF Core

    private Product(Guid vendorId, string name, string description, decimal price, int stockQuantity, Guid categoryId)
    {
        VendorId = vendorId;
        Name = name;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
        CategoryId = categoryId;
    }

    public static Result<Product> Create(Guid vendorId, string name, string description, decimal price, int stockQuantity, Guid categoryId)
    {
        List<Error> errors = [];

        if (vendorId == Guid.Empty)
            errors.Add(ProductErrors.VendorIdRequired);

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(ProductErrors.NameRequired);

        if (name.Length > 200)
            errors.Add(ProductErrors.NameTooLong);

        if (string.IsNullOrWhiteSpace(description))
            errors.Add(ProductErrors.DescriptionRequired);

        if (price <= 0)
            errors.Add(ProductErrors.InvalidPrice);

        if (stockQuantity < 0)
            errors.Add(ProductErrors.InvalidStockQuantity);

        if (categoryId == Guid.Empty)
            errors.Add(ProductErrors.CategoryIdRequired);

        if (errors.Count != 0)
            return errors;

        var product = new Product(vendorId, name.Trim(), description.Trim(), price, stockQuantity, categoryId);

        return product;
    }

    public Result<Updated> UpdateName(string name)
    {
        List<Error> errors = [];

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(ProductErrors.NameRequired);

        if (name.Length > 200)
            errors.Add(ProductErrors.NameTooLong);

        if (errors.Count != 0)
            return errors;

        Name = name.Trim();

        return Result.Updated;
    }

    public Result<Updated> UpdateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            return ProductErrors.DescriptionRequired;

        Description = description.Trim();

        return Result.Updated;
    }

    public Result<Updated> UpdatePrice(decimal price)
    {
        if (price <= 0)
            return ProductErrors.InvalidPrice;

        Price = price;

        return Result.Updated;
    }

    public Result<Updated> UpdateStockQuantity(int stockQuantity)
    {
        if (stockQuantity < 0)
            return ProductErrors.InvalidStockQuantity;

        StockQuantity = stockQuantity;

        return Result.Updated;
    }

    public Result<Updated> ChangeCategory(Category category)
    {
        if (category is null)
            return ProductErrors.CategoryIdRequired;

        Category = category;
        CategoryId = category.Id;

        return Result.Updated;
    }
}

