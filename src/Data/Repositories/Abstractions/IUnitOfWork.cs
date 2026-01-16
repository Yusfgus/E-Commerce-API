namespace E_Commerce.Data.Repositories.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepo { get; }
    IRefreshTokenRepository RefreshTokenRepo { get; }
    IAdminRepository AdminRepo { get; }
    IVendorRepository VendorRepo { get; }
    IProductRepository ProductRepo { get; }
    ICategoryRepository CategoryRepo { get; }
    ICustomerRepository CustomerRepo { get; }
    ICartRepository CartRepo { get; }
    IOrderRepository OrderRepo { get;}

    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
