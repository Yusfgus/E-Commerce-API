using E_Commerce.Requests.Product;
using E_Commerce.Dtos;
using E_Commerce.Results;
using E_Commerce.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using E_Commerce.Models.Auth;
using System.Security.Claims;

namespace E_Commerce.Controllers;

[ApiController]
[Route("api/products")]
[Authorize]
public class ProductController(IProductService productService) : ApiController
{
    // -------------------- PUBLIC --------------------

    [HttpGet]
    public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10, CancellationToken ct = default)
    {
        var result = await productService.GetAllPagedAsync(page, pageSize, ct);

        return result.Match(
            onSuccess: products => Ok(products),
            onFailure: Problem
        );
    }


    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct = default)
    {
        var result = await productService.GetByIdAsync(id, ct);
        
        return result.Match(
            onSuccess: product => Ok(product),
            onFailure: Problem
        );
    }


    [HttpGet("{name:maxlength(20)}")]
    public async Task<IActionResult> GetByName(string name, CancellationToken ct = default)
    {
        var result = await productService.GetByNameAsync(name, ct);

        return result.Match(
            onSuccess: product => Ok(product),
            onFailure: Problem
        );
    }

    // -------------------- VENDOR --------------------

    [HttpPost]
    [Authorize(Roles = nameof(UserRole.Vendor))]
    public async Task<IActionResult> Create(CreateProductRequest request, CancellationToken ct = default)
    {
        Guid vendorId = GetUserId();

        Result<ProductDto> result = await productService.CreateAsync(request, vendorId, ct);

        return result.Match(
            onSuccess: product => CreatedAtAction(nameof(GetById), new { id = product.Id }, product),
            onFailure: Problem
        );
    }


    [HttpPut("{id:guid}")]
    [Authorize(Roles = nameof(UserRole.Vendor))]
    public async Task<IActionResult> Update(Guid id, UpdateProductRequest request, CancellationToken ct = default)
    {
        Guid vendorId = GetUserId();

        var result = await productService.UpdateAsync(id, vendorId, request, ct);

        return result.Match(
            onSuccess: NoContent,
            onFailure: Problem
        );
    }


    [HttpDelete("{id:guid}")]
    [Authorize(Roles = nameof(UserRole.Vendor))]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct = default)
    {
        var result = await productService.DeleteAsync(id, ct);

        return result.Match(
            onSuccess: NoContent,
            onFailure: Problem
        );
    }

}