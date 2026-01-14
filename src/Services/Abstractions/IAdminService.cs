using E_Commerce.Dtos;
using E_Commerce.Results;

namespace E_Commerce.Services.Abstractions;

public interface IAdminService
{
    Task<Result<PaginatedDto<UserDto>>> GetUsersAsync(int page, int pageSize, CancellationToken ct);
    Task<Result<UserDto>> GetUserByIdAsync(Guid id, CancellationToken ct);
    Task<Result> DeactivateUserAsync(Guid id, CancellationToken ct);
    Task<Result> ActivateUserAsync(Guid id, CancellationToken ct);
    Task<Result> DeleteUserAsync(Guid id, CancellationToken ct);
}