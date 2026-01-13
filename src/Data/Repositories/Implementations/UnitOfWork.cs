using E_Commerce.Data.Repositories.Abstractions;

namespace E_Commerce.Data.Repositories.Implementations;

public sealed class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public void Dispose()
    {
        context.Dispose();
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await context.SaveChangesAsync(ct);
    }
}
