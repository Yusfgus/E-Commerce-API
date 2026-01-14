using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using E_Commerce.Data.Repositories.Abstractions;
using E_Commerce.Dtos;
using E_Commerce.Models.Auth;
using E_Commerce.Requests.Auth;
using E_Commerce.Results;
using E_Commerce.Results.Errors;
using Microsoft.IdentityModel.Tokens;

namespace E_Commerce.Services.Identity;

public class JwtTokenProvider(IRefreshTokenRepository refreshTokenRepo,
                              TimeProvider timeProvider,
                              IConfiguration configuration)
{
    public async Task<Result<TokenDto>> CreateAsync(GenerateTokenRequest request, CancellationToken ct)
    {
        // create claims
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, request.UserId.ToString()),
            new (ClaimTypes.Email, request.Email),
            new (ClaimTypes.Role, request.Role.ToString()),
        };

        // foreach(var permission in request.Permissions)
        //     claims.Add(new("permission", permission));

        // ----------

        // get jwtSettings from the configurations
        var jwtSettings = configuration.GetSection("JwtSettings");

        var issuer = jwtSettings["Issuer"]!;
        var audience = jwtSettings["Audience"]!;
        var secretKey = jwtSettings["SecretKey"]!;
        var expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["TokenExpirationInMinutes"]!));

        // ----------

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        // create jwt security token
        var securityToken = new JwtSecurityToken
        (
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        string accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);

        var refreshTokenResult = await CreateRefreshTokenAsync(request.UserId, ct);

        if (refreshTokenResult.IsFailure)
        {
            return refreshTokenResult.Errors;
        }

        return new TokenDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshTokenResult.Value,
            Expires = expires
        };
    }

    public async Task<Result<TokenDto>> RefreshAsync(RefreshTokenRequest request, CancellationToken ct)
    {
        var oldRefreshToken = await refreshTokenRepo.GetByTokenAsync(request.RefreshToken, ct);

        if (oldRefreshToken is null)
        {
            return RefreshTokenErrors.Invalid;
        }

        if (oldRefreshToken.ExpiresOnUtc < timeProvider.GetUtcNow())
        {
            return RefreshTokenErrors.Expired(oldRefreshToken.ExpiresOnUtc.DateTime);
        }

        User user = oldRefreshToken.User;

        // create new token
        var generateTokenRequest = new GenerateTokenRequest
        {
            UserId = user.Id,
            Email = user.Email,
            Role = user.Role,
        };

        return await CreateAsync(generateTokenRequest, ct);
    }

    private async Task<Result<string>> CreateRefreshTokenAsync(Guid userId, CancellationToken ct)
    {
        await refreshTokenRepo.RemoveAllAsync(userId, ct);

        string token = GenerateRefreshToken();

        var refreshTokenResult = RefreshToken.Create
        (
            userId: userId,
            token: token,
            expiresOnUtc: DateTime.UtcNow.AddDays(7)
        );

        if (refreshTokenResult.IsFailure)
        {
            return refreshTokenResult.Errors;
        }

        await refreshTokenRepo.AddAsync(refreshTokenResult.Value!, ct);

        return token;
    }

    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}
