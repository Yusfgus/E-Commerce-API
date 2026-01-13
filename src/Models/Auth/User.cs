using System.Net.Mail;
using System.Text.RegularExpressions;
using E_Commerce.Results.Errors;
using E_Commerce.Models.Common;
using E_Commerce.Results;

namespace E_Commerce.Models.Auth;

public sealed class User : AuditableEntity
{
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public string? PhoneNumber { get; private set; }
    public bool IsActive { get; private set; } = true;
    public DateTimeOffset LastLoginAtUtc { get; private set; }
    public UserRole Role { get; private set; }

    private User() { } // EF Core

    private User(Guid id, string email, string passwordHash, string? phoneNumber, UserRole role)
        : base(id)
    {
        Email = email;
        PasswordHash = passwordHash;
        PhoneNumber = phoneNumber;
        Role = role;
        LastLoginAtUtc = DateTimeOffset.UtcNow;
        IsActive = true;
    }

    public static Result<User> Create(string email, string passwordHash, string? phoneNumber, UserRole role)
    {
        List<Error> errors = [];

        if (string.IsNullOrWhiteSpace(email))
            errors.Add(UserErrors.EmailRequired);

        if(!EmailValidation(email))
            errors.Add(UserErrors.EmailInvalid);

        if (string.IsNullOrWhiteSpace(passwordHash))
            errors.Add(UserErrors.PasswordHashRequired);

        if (!string.IsNullOrWhiteSpace(phoneNumber) && !Regex.IsMatch(phoneNumber, @"^\+[1-9]\d{7,14}$"))
            errors.Add(UserErrors.PhoneNumberInvalid);

        if (errors.Count != 0)
            return errors;

        var user = new User(
            Guid.NewGuid(),
            email.Trim().ToLowerInvariant(),
            passwordHash,
            phoneNumber,
            role);

        return user;
    }

    private static bool EmailValidation(string email)
    {
        try
        {
            _ = new MailAddress(email);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public Result<Updated> UpdateEmail(string email)
    {
        List<Error> errors = [];

        if (string.IsNullOrWhiteSpace(email))
            errors.Add(UserErrors.EmailRequired);

        if (!EmailValidation(email))
            errors.Add(UserErrors.EmailInvalid);

        if (errors.Count != 0)
            return errors;

        Email = email.Trim().ToLowerInvariant();
        return Result.Updated;
    }

    public Result<Updated> UpdatePhoneNumber(string phoneNumber)
    {
        if (!string.IsNullOrWhiteSpace(phoneNumber) || !Regex.IsMatch(phoneNumber, @"^\+?\d{7,15}$"))
        {
            return UserErrors.PhoneNumberInvalid;
        }

        return Result.Updated;
    }

    public Result<Updated> ChangePassword(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            return UserErrors.PasswordHashRequired;

        PasswordHash = passwordHash;
        return Result.Updated;
    }

    public void UpdateLastLogin()
    {
        LastLoginAtUtc = DateTimeOffset.UtcNow;
    }

    public Result<Updated> Deactivate()
    {
        if (!IsActive)
            return UserErrors.UserInactive;

        IsActive = false;
        return Result.Updated;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void ChangeRole(UserRole role)
    {
        Role = role;
    }
}
