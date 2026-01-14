using E_Commerce.Models.Products;

namespace E_Commerce.Data.Repositories.Abstractions;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync(int page, int pageSize, CancellationToken ct);
    Task<Category?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<Category?> GetByNameAsync(string category, CancellationToken ct);
    Task AddAsync(Category category, CancellationToken ct);
    Task<bool> IsExit(string category, CancellationToken ct);
    Task<bool> HasProductsAsync(Guid id, CancellationToken ct);
    void Remove(Category category);
    Task<Category?> GetByIdAsTrackingAsync(Guid id, CancellationToken ct);
}