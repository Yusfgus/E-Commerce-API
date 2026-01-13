using E_Commerce.Models.Products;

namespace E_Commerce.Data.Repositories.Abstractions;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<Product?> GetByNameAsync(string name, CancellationToken ct);
    Task<Product?> GetByIdAsTrackingAsync(Guid id, CancellationToken ct);
    Task<List<Product>> GetAllPagedAsync(int page, int pageSize, CancellationToken ct);
    Task<int> CountAsync(CancellationToken ct);
    Task AddAsync(Product product, CancellationToken ct);
    Task<bool> IsExistAsync(Guid id, CancellationToken ct);
    Task<bool> IsExistAsync(string name, CancellationToken ct);
    Task RemoveAsync(Product product, CancellationToken ct);
}