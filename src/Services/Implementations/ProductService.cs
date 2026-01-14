using E_Commerce.Mappers;
using E_Commerce.Data.Repositories.Abstractions;
using E_Commerce.Requests.Product;
using E_Commerce.Dtos;
using E_Commerce.Results;
using E_Commerce.Services.Abstractions;
using E_Commerce.Models.Products;
using E_Commerce.Results.Errors;

namespace E_Commerce.Services.Implementations;

public class ProductService(IProductRepository productRepo,
                            ICategoryRepository categoryRepo,
                            IUserRepository userRepo,
                            IUnitOfWork uow)
    : IProductService
{
    public async Task<Result<PaginatedDto<ProductDto>>> GetAllPagedAsync(int page, int pageSize, CancellationToken ct)
    {
        List<Product> products = await productRepo.GetAllPagedAsync(page, pageSize, ct);
        int totalCount = await productRepo.CountAsync(ct);

        return PaginatedDto<ProductDto>.Create(
            page,
            pageSize,
            products.ToDto(),
            totalCount);
    }

    public async Task<Result<ProductDto>> GetByIdAsync(Guid id, CancellationToken ct)
    {
        Product? product = await productRepo.GetByIdAsync(id, ct);

        if (product is null)
            return ProductErrors.NotFound(id);

        return product.ToDto();
    }

    public async Task<Result<ProductDto>> GetByNameAsync(string name, CancellationToken ct)
    {
        Product? product = await productRepo.GetByNameAsync(name, ct);

        if (product is null)
            return ProductErrors.NotFound(name);

        return product.ToDto();
    }

    public async Task<Result<ProductDto>> CreateAsync(CreateProductRequest request, Guid vendorId, CancellationToken ct)
    {
        if (!await userRepo.IsExist(vendorId, ct))
            return ProductErrors.VendorNotFound(vendorId);

        if (await productRepo.IsExistAsync(request.Name!, ct))
            return ProductErrors.NameInUse(request.Name!);

        Category? category = await categoryRepo.GetByNameAsync(request.Category!, ct);

        if (category is null)
            return CategoryErrors.NotFound(request.Category!);

        var productResult = Product.Create
        (
            vendorId: vendorId,
            name: request.Name!,
            description: request.Description!,
            price: request.Price,
            stockQuantity: request.StockQuantity,
            // Category = category, // EF will try to add the category again (as it isn't tracked)
            categoryId: category.Id // works with EF
        );

        if (productResult.IsFailure)
        {
            return productResult.Errors;
        }

        Product product = productResult.Value!;

        await productRepo.AddAsync(product, ct);

        await uow.SaveChangesAsync(ct);

        // product.Category = category; // for response

        return product.ToDto();
    }

    public async Task<Result> UpdateAsync(Guid id, Guid currentVendorId, UpdateProductRequest request, CancellationToken ct)
    {
        var product = await productRepo.GetByIdAsTrackingAsync(id, ct);

        if (product is null)
            return ProductErrors.NotFound(id);

        if (product.VendorId != currentVendorId)
            return ProductErrors.ProductAccessDenied;

        List<List<Error>> errors = [];

        if (request.Category is not null)
        {
            var category = await categoryRepo.GetByNameAsync(request.Category, ct);

            if (category is null)
                return CategoryErrors.NotFound(request.Category);

            var result = product.ChangeCategory(category);
            if (result.IsFailure)
                errors.Add(result.Errors);
        }

        if (request.Name is not null)
        {
            var result = product.UpdateName(request.Name);
            if (result.IsFailure)
                errors.Add(result.Errors);
        }

        if (request.Description is not null)
        {
            var result = product.UpdateDescription(request.Description);
            if (result.IsFailure)
                errors.Add(result.Errors);
        }

        if (request.Price is not null)
        {
            var result = product.UpdatePrice(request.Price.Value);
            if (result.IsFailure)
                errors.Add(result.Errors);
        }

        if (request.StockQuantity is not null)
        {
            var result = product.UpdateStockQuantity(request.StockQuantity.Value);
            if (result.IsFailure)
                errors.Add(result.Errors);
        }

        if (errors.Count != 0)
        {
            return errors.SelectMany(e => e).ToList();
        }

        await uow.SaveChangesAsync(ct);
        
        return Result.Success;
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken ct)
    {
        Product? product = await productRepo.GetByIdAsync(id, ct);

        if (product is null)
            return ProductErrors.NotFound(id);

        await productRepo.RemoveAsync(product, ct);

        await uow.SaveChangesAsync(ct);

        return Result.Success;
    }

}