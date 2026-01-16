using E_Commerce.Data.Repositories.Abstractions;
using E_Commerce.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Repositories.Implementations;

public class CustomerRepository(AppDbContext context) : ICustomerRepository
{
    public async Task AddAsync(Customer customer, CancellationToken ct)
    {
        await context.Customers.AddAsync(customer, ct);
    }

    public async Task<bool> IsExist(Guid id, CancellationToken ct)
    {
        return await context.Customers.AnyAsync(c => c.UserId == id, ct);
    }
}
