using E_Commerce.Requests.Product;
using E_Commerce.Dtos;
using E_Commerce.Results;
using E_Commerce.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController(IProductService productService) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
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


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken ct = default)
    {
        Result<ProductDto> result = await productService.CreateAsync(request, ct);

        return result.Match<IActionResult>(
            onSuccess: product => CreatedAtAction(nameof(GetById), new { id = product.Id }, product),
            onFailure: Problem
        );
    }


    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequest request, CancellationToken ct = default)
    {
        var result = await productService.UpdateAsync(id, request, ct);

        return result.Match<IActionResult>(
            onSuccess: _ => NoContent(),
            onFailure: Problem
        );
    }


    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct = default)
    {
        var result = await productService.DeleteAsync(id, ct);

        return result.Match<IActionResult>(
            onSuccess: _ => NoContent(),
            onFailure: Problem
        );
    }

}