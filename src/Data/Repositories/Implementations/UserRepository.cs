using E_Commerce.Data;using E_Commerce.Data.Repositories.Abstractions;
using E_Commerce.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Repositories.Implementations;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task AddAsync(User user, CancellationToken ct)
    {
        await context.Users.AddAsync(user, ct);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email == email, ct);
    }
    
    public async Task<User?> GetByEmailAsTrackingAsync(string email, CancellationToken ct)
    {
        return await context.Users.AsTracking().FirstOrDefaultAsync(u => u.Email == email, ct);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Id == id, ct);
    }

    public async Task<User?> GetByIdAsTrackingAsync(Guid id, CancellationToken ct)
    {
        return await context.Users.AsTracking().FirstOrDefaultAsync(u => u.Id == id, ct);
    }

    public async Task<bool> IsEmailExist(string? email, CancellationToken ct)
    {
        return await context.Users.AnyAsync(u => u.Email == email, ct);
    }

    public async Task<List<User>> GetAllPagedAsync(int page, int pageSize, CancellationToken ct)
    {
        return await context.Users
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync(ct);
    }

    public async Task<int> CountAsync(CancellationToken ct)
    {
        return await context.Users.CountAsync(ct);
    }

    public async Task<bool> IsExist(Guid id, CancellationToken ct)
    {
        return await context.Users.AnyAsync(u => u.Id == id, ct);
    }

    public async Task<bool> IsPhoneNumberExist(string phoneNumber, CancellationToken ct)
    {
        return await context.Users.AnyAsync(u => u.PhoneNumber == phoneNumber, ct);
    }

    public Task RemoveAsync(User user, CancellationToken ct)
    {
        context.Users.Remove(user);
        return Task.CompletedTask;
    }

}