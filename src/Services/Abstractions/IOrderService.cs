using E_Commerce.Dtos;
using E_Commerce.Results;

namespace E_Commerce.Services.Abstractions;

public interface IOrderService
{
    Task<Result<OrderDto>> GetByIdAsync(Guid id, CancellationToken ct);
    Task<Result<PaginatedDto<OrderDto>>> GetByCustomerIdAsync(Guid customerId,
                                                                        int page,
                                                                        int pageSize,
                                                                        CancellationToken ct);
    Task<Result<OrderDto>> CheckoutAsync(Guid customerId, CancellationToken ct);
}