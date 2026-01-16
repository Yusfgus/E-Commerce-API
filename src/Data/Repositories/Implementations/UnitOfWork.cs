using E_Commerce.Data.Repositories.Abstractions;

namespace E_Commerce.Data.Repositories.Implementations;

public sealed class UnitOfWork(AppDbContext context, IUserRepository userRepo, IRefreshTokenRepository refreshTokenRepo, IAdminRepository adminRepo, IVendorRepository vendorRepo, IProductRepository productRepo, ICategoryRepository categoryRepo, ICustomerRepository customerRepo, ICartRepository cartRepo, IOrderRepository orderRepo) : IUnitOfWork
{
    public AppDbContext _context = context;
    public IUserRepository UserRepo { get; } = userRepo;
    public IRefreshTokenRepository RefreshTokenRepo { get; } = refreshTokenRepo;
    public IAdminRepository AdminRepo { get; } = adminRepo;
    public IVendorRepository VendorRepo { get; } = vendorRepo;
    public IProductRepository ProductRepo { get; } = productRepo;
    public ICategoryRepository CategoryRepo { get; } = categoryRepo;
    public ICustomerRepository CustomerRepo { get; } = customerRepo;
    public ICartRepository CartRepo { get; } = cartRepo;
    public IOrderRepository OrderRepo { get; } = orderRepo;

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await _context.SaveChangesAsync(ct);
    }
}
