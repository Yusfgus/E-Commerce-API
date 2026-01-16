using E_Commerce.Data.Repositories.Abstractions;
using E_Commerce.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Repositories.Implementations;

public class VendorRepository(AppDbContext context) : IVendorRepository
{
    public async Task AddAsync(Vendor vendor, CancellationToken ct)
    {
        await context.Vendors.AddAsync(vendor, ct);
    }

    public async Task<bool> IsExist(Guid id, CancellationToken ct)
    {
        return await context.Vendors.AnyAsync(v => v.UserId == id, ct);
    }
}