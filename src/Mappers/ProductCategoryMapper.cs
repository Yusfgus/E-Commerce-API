using E_Commerce.Dtos;
using E_Commerce.Models.Products;

namespace E_Commerce.Mappers;

public static class CategoryMapper
{
    public static CategoryDto ToDto(this Category category)
    {
        ArgumentNullException.ThrowIfNull(category);

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public static List<CategoryDto> ToDto(this IEnumerable<Category> categories)
    {
        ArgumentNullException.ThrowIfNull(categories);

        return categories.Select(ToDto).ToList();
    }
}