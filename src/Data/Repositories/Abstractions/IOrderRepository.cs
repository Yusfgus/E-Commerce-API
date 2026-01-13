using E_Commerce.Models.Orders;

namespace E_Commerce.Data.Repositories.Abstractions;

public interface IOrderRepository
{
    Task<List<Order>> GetByCustomerIdAsync(Guid customerId,
                                            int page,
                                            int pageSize,
                                            CancellationToken ct);

    Task<int> CountAsync(Guid customerId, CancellationToken ct);
    Task<Order?> GetByIdAsync(Guid id, CancellationToken ct);
}