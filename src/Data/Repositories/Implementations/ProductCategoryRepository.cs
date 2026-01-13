using E_Commerce.Data;using E_Commerce.Data.Repositories.Abstractions;
using E_Commerce.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Repositories.Implementations;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    public async Task<bool> IsExit(string category, CancellationToken ct)
    {
        return await context.Categories.AnyAsync(pc => pc.Name == category, ct);
    }

    public async Task<Category?> GetByNameAsync(string category, CancellationToken ct)
    {
        return await context.Categories.FirstOrDefaultAsync(pc => pc.Name == category, ct);
    }
}