using E_Commerce.Data;using E_Commerce.Data.Repositories.Abstractions;
using E_Commerce.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Repositories.Implementations;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    public async Task AddAsync(Product product, CancellationToken ct)
    {
        await context.Products.AddAsync(product, ct);
    }

    public async Task<int> CountAsync(CancellationToken ct)
    {
        return await context.Products.CountAsync(ct);
    }

    public async Task<List<Product>> GetAllPagedAsync(int page, int pageSize, CancellationToken ct)
    {
        return await context.Products
                            .Include(p => p.Category)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync(ct);
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await context.Products
                            .Include(p => p.Category)
                            .FirstOrDefaultAsync(p => p.Id == id, ct);
    }
    
    public async Task<Product?> GetByIdAsTrackingAsync(Guid id, CancellationToken ct)
    {
        return await context.Products.AsTracking()
                            .Include(p => p.Category)
                            .FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public async Task<Product?> GetByNameAsync(string name, CancellationToken ct)
    {
        return await context.Products
                            .Include(p => p.Category)
                            .FirstOrDefaultAsync(p => p.Name == name, ct);
    }

    public async Task<bool> IsExistAsync(Guid id, CancellationToken ct)
    {
        return await context.Products.AnyAsync(p => p.Id == id, ct);
    }

    public async Task<bool> IsExistAsync(string name, CancellationToken ct)
    {
        return await context.Products.AnyAsync(p => p.Name == name, ct);
    }

    public Task RemoveAsync(Product product, CancellationToken ct)
    {
        context.Products.Remove(product);
        return Task.CompletedTask;
    }

}