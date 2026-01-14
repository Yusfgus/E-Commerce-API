using E_Commerce.Data.Repositories.Abstractions;
using E_Commerce.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Repositories.Implementations;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    public async Task<bool> IsExit(string category, CancellationToken ct)
    {
        return await context.Categories.AnyAsync(pc => pc.Name == category, ct);
    }

    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await context.Categories.FirstOrDefaultAsync(pc => pc.Id == id, ct);
    }

    public async Task<Category?> GetByIdAsTrackingAsync(Guid id, CancellationToken ct)
    {
        return await context.Categories.AsTracking().FirstOrDefaultAsync(pc => pc.Id == id, ct);
    }

    public async Task<Category?> GetByNameAsync(string category, CancellationToken ct)
    {
        return await context.Categories.FirstOrDefaultAsync(pc => pc.Name == category, ct);
    }

    public async Task<List<Category>> GetAllAsync(int page, int pageSize, CancellationToken ct)
    {
        return await context.Categories.Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync(ct);
    }

    public async Task AddAsync(Category category, CancellationToken ct)
    {
        await context.Categories.AddAsync(category, ct);
    }

    public async Task<bool> HasProductsAsync(Guid categoryId, CancellationToken ct)
    {
        return await context.Products.AnyAsync(p => p.CategoryId == categoryId, ct);
    }

    public void Remove(Category category)
    {
        context.Categories.Remove(category);
    }
}