using E_Commerce.Dtos;
using E_Commerce.Results;

namespace E_Commerce.Services.Abstractions;

public interface IAdminService
{
    Task<Result<PaginatedDto<UserDto>>> GetUsersAsync(int page, int pageSize, CancellationToken ct);
    Task<Result<UserDto>> GetUserByIdAsync(Guid id, CancellationToken ct);
    Task<Result<Updated>> DeactivateUserAsync(Guid id, CancellationToken ct);
    Task<Result<Updated>> ActivateUserAsync(Guid id, CancellationToken ct);
    Task<Result<Deleted>> DeleteUserAsync(Guid id, CancellationToken ct);
}