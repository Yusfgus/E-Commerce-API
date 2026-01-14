using E_Commerce.Models.Products;
using E_Commerce.Results;
using E_Commerce.Results.Errors;

namespace E_Commerce.Models.Auth;



public sealed class Vendor
{
    public Guid UserId { get; private set; }
    public string StoreName { get; private set; } = null!;
    // rating
    // description
    // followers
    // orders

    // Navigation
    public User User { get; private set; } = null!;
    public ICollection<Product>? Products { get; private set; }

    private Vendor() { } // EF Core

    private Vendor(Guid userId, string storeName)
    {
        UserId = userId;
        StoreName = storeName;
    }

    public static Result<Vendor> Create(Guid userId, string? storeName)
    {
        if (userId == Guid.Empty)
            return VendorErrors.UserIdRequired;

        if (string.IsNullOrWhiteSpace(storeName))
            return VendorErrors.StoreNameRequired;

        if (storeName.Length > 100)
            return VendorErrors.StoreNameTooLong;

        var vendor = new Vendor(userId, storeName.Trim());

        return vendor;
    }

    public Result UpdateStoreName(string? storeName)
    {
        if (string.IsNullOrWhiteSpace(storeName))
            return VendorErrors.StoreNameRequired;

        if (storeName.Length > 100)
            return VendorErrors.StoreNameTooLong;

        StoreName = storeName.Trim();

        return Result.Success;
    }

}
