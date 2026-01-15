using E_Commerce.Data.Repositories.Abstractions;
using E_Commerce.Dtos;
using E_Commerce.Mappers;
using E_Commerce.Models.Products;
using E_Commerce.Requests.Product;
using E_Commerce.Results;
using E_Commerce.Results.Errors;
using E_Commerce.Services.Abstractions;

namespace E_Commerce.Services.Implementations;

public class CategoryService(ICategoryRepository categoryRepo, IUnitOfWork uow) : ICategoryService
{
    public async Task<Result<List<CategoryDto>>> GetAllAsync(int page, int pageSize, CancellationToken ct)
    {
        List<Category> categories = await categoryRepo.GetAllAsync(page, pageSize, ct);

        return categories.ToDto();
    }

    public async Task<Result<CategoryDto>> GetByIdAsync(Guid id, CancellationToken ct)
    {
        Category? category = await categoryRepo.GetByIdAsync(id, ct);

        if (category is null)
            return CategoryErrors.NotFound(id);

        return category.ToDto();
    }

    public async Task<Result<CategoryDto>> GetByNameAsync(String name, CancellationToken ct)
    {
        Category? category = await categoryRepo.GetByNameAsync(name, ct);

        if (category is null)
            return CategoryErrors.NotFound(name);

        return category.ToDto();
    }

    public async Task<Result<CategoryDto>> CreateAsync(CreateCategoryRequest request, CancellationToken ct)
    {
        if (await categoryRepo.IsExit(request.Name, ct))
            return CategoryErrors.NameTaken;

        var categoryResult = Category.Create(request.Name);

        if (categoryResult.IsFailure)
        {
            return categoryResult.Errors;
        }

        Category category = categoryResult.Value!;

        await categoryRepo.AddAsync(category, ct);

        await uow.SaveChangesAsync(ct);

        return category.ToDto();
    }

    public async Task<Result> UpdateAsync(Guid id, UpdateCategoryRequest request, CancellationToken ct)
    {
        Category? category = await categoryRepo.GetByIdAsTrackingAsync(id, ct);

        if (category is null)
            return CategoryErrors.NotFound(id);

        List<List<Error>> errors = [];

        if (request.Name is not null)
        {
            if(await categoryRepo.IsExit(request.Name, ct))
                return CategoryErrors.NameTaken;

            var result = category.UpdateName(request.Name);
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
        Category? category = await categoryRepo.GetByIdAsync(id, ct);

        if (category is null)
            return CategoryErrors.NotFound(id);

        if (await categoryRepo.HasProductsAsync(id, ct))
            return CategoryErrors.HasProducts;

        categoryRepo.Remove(category);

        await uow.SaveChangesAsync(ct);

        return Result.Success;
    }
}
