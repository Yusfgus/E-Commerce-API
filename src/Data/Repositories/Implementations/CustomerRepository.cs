using E_Commerce.Data.Repositories.Abstractions;
using E_Commerce.Models.Auth;

namespace E_Commerce.Data.Repositories.Implementations;

public class CustomerRepository(AppDbContext context) : ICustomerRepository
{
    public async Task AddAsync(Customer customer, CancellationToken ct)
    {
        await context.Customers.AddAsync(customer, ct);
    }
}
