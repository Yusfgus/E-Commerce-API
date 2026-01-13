using E_Commerce.Dtos;
using E_Commerce.Models.Products;

namespace E_Commerce.Mappers;

public static class ProductMapper
{
    public static ProductDto ToDto(this Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            Category = product.Category.Name
        };
    }

    public static List<ProductDto> ToDto(this IEnumerable<Product> products)
    {
        ArgumentNullException.ThrowIfNull(products);

        return products.Select(ToDto).ToList();
    }
}