using E_Commerce.Models.Auth;

namespace E_Commerce.Data.Repositories.Abstractions;

public interface IVendorRepository
{
    Task AddAsync(Vendor customer, CancellationToken ct);
    Task<bool> IsExist(Guid id, CancellationToken ct);
}
