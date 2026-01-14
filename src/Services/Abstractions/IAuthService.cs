using E_Commerce.Request.Auth;
using E_Commerce.Dtos;
using E_Commerce.Results;
using E_Commerce.Requests.Auth;

namespace E_Commerce.Services.Abstractions;

public interface IAuthService
{
    Task<Result<UserDto>> RegisterCustomerAsync(RegisterCustomerRequest request, CancellationToken ct);
    Task<Result<UserDto>> RegisterVendorAsync(RegisterVendorRequest request, CancellationToken ct);
    Task<Result<TokenDto>> LogInAsync(LogInUserRequest request, CancellationToken ct);
    Task<Result> LogOutAsync(CancellationToken ct);
    Task<Result<TokenDto>> Refresh(RefreshTokenRequest request, CancellationToken ct);
}
