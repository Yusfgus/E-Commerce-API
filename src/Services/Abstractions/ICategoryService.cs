using E_Commerce.Dtos;
using E_Commerce.Requests.Product;
using E_Commerce.Results;

namespace E_Commerce.Services.Abstractions;

public interface ICategoryService
{
    Task<Result<List<CategoryDto>>> GetAllAsync(int page, int pageSize, CancellationToken ct);
    Task<Result<CategoryDto>> GetByIdAsync(Guid id, CancellationToken ct);
    Task<Result<CategoryDto>> CreateAsync(CreateCategoryRequest request, CancellationToken ct);
    Task<Result> DeleteAsync(Guid id, CancellationToken ct);
    Task<Result> UpdateAsync(Guid id, UpdateCategoryRequest request, CancellationToken ct);
}