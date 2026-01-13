using E_Commerce.Data.Repositories.Abstractions;
using E_Commerce.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Repositories.Implementations;

public class RefreshTokenRepository(AppDbContext context) : IRefreshTokenRepository
{
    public async Task AddAsync(RefreshToken refreshToken, CancellationToken ct)
    {
        await context.RefreshTokens.AddAsync(refreshToken, ct);
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken ct)
    {
        return await context.RefreshTokens
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Token == token, ct);
    }

    public async Task RemoveAllAsync(Guid userId, CancellationToken ct)
    {
        await context.RefreshTokens
              .Where(rt => rt.UserId == userId)
              .ExecuteDeleteAsync(ct);
    }
}