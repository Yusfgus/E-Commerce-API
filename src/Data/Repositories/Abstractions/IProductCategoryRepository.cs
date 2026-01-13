using E_Commerce.Models.Products;

namespace E_Commerce.Data.Repositories.Abstractions;

public interface ICategoryRepository
{
    Task<Category?> GetByNameAsync(string category, CancellationToken ct);
    Task<bool> IsExit(string category, CancellationToken ct);
}