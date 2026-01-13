using E_Commerce.Results;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Dtos;

public sealed class ApiResponse
{
    public bool Success { get; private set; } = false;
    public object? Data { get; private set; } = null;
    public ProblemDetails? Error { get; private set; } = null;
    public IDictionary<string, string[]>? Errors { get; private set; } = null;
    public string TraceId { get; private set; } = null!;

    private ApiResponse()
    {
    }

    public static ApiResponse Create(object? value, string traceId)
    {
        var response = new ApiResponse()
        {
            TraceId = traceId
        };

        if (value is ValidationProblemDetails validation)
        {
            response.Errors = validation.Errors;
        }
        else if (value is ProblemDetails problem)
        {
            problem.Extensions.Remove("traceId");
            response.Error = problem;
        }
        else
        {
            response.Success = true;
            response.Data = value;
        }

        return response;
    }
}
