using E_Commerce.Data.Repositories.Abstractions;
using E_Commerce.Models.Auth;

namespace E_Commerce.Data.Repositories.Implementations;

public class VendorRepository(AppDbContext context) : IVendorRepository
{
    public async Task AddAsync(Vendor vendor, CancellationToken ct)
    {
        await context.Vendors.AddAsync(vendor, ct);
    }
}