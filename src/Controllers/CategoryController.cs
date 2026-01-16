using E_Commerce.Controllers;
using E_Commerce.Requests.Product;
using E_Commerce.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/categories")]
[Authorize]
public class CategoriesController(ICategoryService categoryService) : ApiController
{
    // -------------------- PUBLIC --------------------

    [HttpGet]
    public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10, CancellationToken ct = default)
    {
        var result = await categoryService.GetAllAsync(page, pageSize, ct);

        return result.Match(
            onSuccess: Ok,
            onFailure: Problem
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct = default)
    {
        var result = await categoryService.GetByIdAsync(id, ct);

        return result.Match(
            onSuccess: Ok,
            onFailure: Problem
        );
    }

    // -------------------- ADMIN --------------------

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateCategoryRequest request, CancellationToken ct = default)
    {
        var result = await categoryService.CreateAsync(request, ct);

        return result.Match(
            onSuccess: category => CreatedAtAction(
                nameof(GetById),
                new { id = category.Id },
                category
            ),
            onFailure: Problem
        );
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, UpdateCategoryRequest request, CancellationToken ct = default)
    {
        var result = await categoryService.UpdateAsync(id, request, ct);

        return result.Match(
            onSuccess: NoContent,
            onFailure: Problem
        );
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct = default)
    {
        var result = await categoryService.DeleteAsync(id, ct);

        return result.Match(
            onSuccess: NoContent,
            onFailure: Problem
        );
    }
}
