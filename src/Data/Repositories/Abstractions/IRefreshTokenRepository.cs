using E_Commerce.Models.Auth;

namespace E_Commerce.Data.Repositories.Abstractions;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken refreshToken, CancellationToken ct);
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken ct);
    Task RemoveAllAsync(Guid userId, CancellationToken ct);
}