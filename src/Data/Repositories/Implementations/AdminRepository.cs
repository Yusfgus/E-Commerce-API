using E_Commerce.Data.Repositories.Abstractions;
using E_Commerce.Dtos;
using E_Commerce.Models.Auth;
using E_Commerce.Results;

namespace E_Commerce.Data.Repositories.Implementations;

public class AdminRepository(AppDbContext context) : IAdminRepository
{
    public async Task AddAsync(Admin admin, CancellationToken ct)
    {
        await context.Admins.AddAsync(admin, ct);
    }
}