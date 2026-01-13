using E_Commerce.Models.Common;
using E_Commerce.Results;
using E_Commerce.Results.Errors;

namespace E_Commerce.Models.Auth;

public sealed class RefreshToken : AuditableEntity
{
    public Guid UserId { get; private set; }
    public string Token { get; private set; } = null!;
    public DateTimeOffset ExpiresOnUtc { get; private set; }

    // Navigation
    public User User { get; private set; } = null!;

    private RefreshToken() { } // EF Core

    private RefreshToken(Guid userId, string token, DateTimeOffset expiresOnUtc)
    {
        UserId = userId;
        Token = token;
        ExpiresOnUtc = expiresOnUtc;
    }

    public static Result<RefreshToken> Create(Guid userId, string? token, DateTimeOffset expiresOnUtc)
    {
        if (userId == Guid.Empty)
            return RefreshTokenErrors.UserIdRequired;

        if (string.IsNullOrWhiteSpace(token))
            return RefreshTokenErrors.TokenRequired;

        if (expiresOnUtc <= DateTimeOffset.UtcNow)
            return RefreshTokenErrors.ExpiryInPast;

        var refreshToken = new RefreshToken(
            userId,
            token.Trim(),
            expiresOnUtc);

        return refreshToken;
    }
}
