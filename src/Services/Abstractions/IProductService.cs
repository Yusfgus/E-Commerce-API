using E_Commerce.Requests.Product;
using E_Commerce.Dtos;
using E_Commerce.Results;

namespace E_Commerce.Services.Abstractions;

public interface IProductService
{
    Task<Result<PaginatedDto<ProductDto>>> GetAllPagedAsync(int page, int pageSize, CancellationToken ct);
    Task<Result<ProductDto>> GetByIdAsync(Guid id, CancellationToken ct);
    Task<Result<ProductDto>> GetByNameAsync(string name, CancellationToken ct);
    Task<Result<ProductDto>> CreateAsync(CreateProductRequest request, Guid vendorId, CancellationToken ct);
    Task<Result> UpdateAsync(Guid id, Guid currentVendorId, UpdateProductRequest request, CancellationToken ct);
    Task<Result> DeleteAsync(Guid id, CancellationToken ct);
}
