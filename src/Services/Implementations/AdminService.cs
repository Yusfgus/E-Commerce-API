using E_Commerce.Data.Repositories.Abstractions;
using E_Commerce.Dtos;
using E_Commerce.Results.Errors;
using E_Commerce.Mappers;
using E_Commerce.Models.Auth;
using E_Commerce.Results;
using E_Commerce.Services.Abstractions;

namespace E_Commerce.Services.Implementations;

public class AdminService(IUnitOfWork uow) : IAdminService
{
    public async Task<Result<PaginatedDto<UserDto>>> GetUsersAsync(int page, int pageSize, CancellationToken ct)
    {
        List<User> users = await uow.UserRepo.GetAllPagedAsync(page, pageSize, ct);
        
        int totalCount = await uow.UserRepo.CountAsync(ct);

        return PaginatedDto<UserDto>.Create(
            page,
            pageSize,
            users.ToDto(),
            totalCount);
    }

    public async Task<Result<UserDto>> GetUserByIdAsync(Guid id, CancellationToken ct)
    {
        User? user = await uow.UserRepo.GetByIdAsync(id, ct);

        return user is null 
                ? UserErrors.NotFound(id)
                : user.ToDto();
    }

    public async Task<Result> DeleteUserAsync(Guid id, CancellationToken ct)
    {
        User? user = await uow.UserRepo.GetByIdAsync(id, ct);

        if (user is null)
            return UserErrors.NotFound(id);

        await uow.UserRepo.RemoveAsync(user, ct);

        await uow.SaveChangesAsync(ct);

        return Result.Success;
    }
    
    public async Task<Result> DeactivateUserAsync(Guid id, CancellationToken ct)
    {
        User? user = await uow.UserRepo.GetByIdAsTrackingAsync(id, ct);

        if (user is null)
            return UserErrors.NotFound(id);

        var result = user.Deactivate();

        if (result.IsFailure)
        {
            return result.Errors;
        }

        await uow.SaveChangesAsync(ct);

        return Result.Success;
    }

    public async Task<Result> ActivateUserAsync(Guid id, CancellationToken ct)
    {
        User? user = await uow.UserRepo.GetByIdAsync(id, ct);

        if (user is null)
            return UserErrors.NotFound(id);

        user.Activate();

        await uow.SaveChangesAsync(ct);

        return Result.Success;
    }

}