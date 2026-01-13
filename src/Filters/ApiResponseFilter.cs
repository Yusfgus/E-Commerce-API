using E_Commerce.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace E_Commerce.Filters;

public sealed class ApiResponseFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is not ObjectResult objectResult)
            return;

        var response = ApiResponse.Create(objectResult.Value, context.HttpContext.TraceIdentifier);

        context.Result = new ObjectResult(response)
        {
            StatusCode = objectResult.StatusCode
        };
    }

    public void OnResultExecuted(ResultExecutedContext context) { }
}
