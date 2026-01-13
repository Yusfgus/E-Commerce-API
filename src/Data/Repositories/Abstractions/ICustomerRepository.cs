using E_Commerce.Models.Auth;

namespace E_Commerce.Data.Repositories.Abstractions;

public interface ICustomerRepository
{
    Task AddAsync(Customer customer, CancellationToken ct);
}
