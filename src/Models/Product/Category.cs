using E_Commerce.Models.Common;

namespace E_Commerce.Models.Products;

public sealed class Category : Entity
{
    public string Name { get; set; } = null!;

    // Navigation
    public ICollection<Product> Products { get; set; }  = new List<Product>();
}