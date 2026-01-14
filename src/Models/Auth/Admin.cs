using E_Commerce.Results;
using E_Commerce.Results.Errors;

namespace E_Commerce.Models.Auth;

public sealed class Admin
{
    public Guid UserId { get; private set; }
    public DateTimeOffset LastActionAtUtc { get; private set; }
    // level
    // permissions

    // Navigation
    public User User { get; private set; } = null!;

    private Admin() { } // EF Core

    private Admin(Guid userId, DateTimeOffset lastActionAtUtc)
    {
        UserId = userId;
        LastActionAtUtc = lastActionAtUtc;
    }

    public static Result<Admin> Create(Guid userId, DateTimeOffset? lastActionAtUtc = null)
    {
        if (userId == Guid.Empty)
            return AdminErrors.UserIdRequired;

        var actionTime = lastActionAtUtc ?? DateTimeOffset.UtcNow;

        if (actionTime > DateTimeOffset.UtcNow)
            return AdminErrors.LastActionInFuture;

        var admin = new Admin(userId, actionTime);

        return admin;
    }

    public Result UpdateLastAction(DateTimeOffset actionAtUtc)
    {
        if (actionAtUtc > DateTimeOffset.UtcNow)
            return AdminErrors.LastActionInFuture;

        LastActionAtUtc = actionAtUtc;
        return Result.Success;
    }
}