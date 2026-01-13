using E_Commerce.Models.Auth;

namespace E_Commerce.Data.Repositories.Abstractions;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<User?> GetByIdAsTrackingAsync(Guid id, CancellationToken ct);
    Task<User?> GetByEmailAsync(string email, CancellationToken ct);
    Task<User?> GetByEmailAsTrackingAsync(string email, CancellationToken ct);
    Task<List<User>> GetAllPagedAsync(int page, int pageSize, CancellationToken ct);
    Task<int> CountAsync(CancellationToken ct);
    Task AddAsync(User user, CancellationToken ct);
    Task RemoveAsync(User user, CancellationToken ct);
    Task<bool> IsExist(Guid id, CancellationToken ct);
    Task<bool> IsEmailExist(string? email, CancellationToken ct);
    Task<bool> IsPhoneNumberExist(string phoneNumber, CancellationToken ct);
}
