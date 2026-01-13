using E_Commerce.Mappers;
using E_Commerce.Data.Repositories.Abstractions;
using E_Commerce.Dtos;
using E_Commerce.Results;
using E_Commerce.Services.Abstractions;
using E_Commerce.Models.Orders;
using E_Commerce.Results.Errors;

namespace E_Commerce.Services.Implementations;

public class OrderService(IOrderRepository orderRepo, IUserRepository customerRepo) : IOrderService
{
    public async Task<Result<OrderDto>> GetByIdAsync(Guid id, CancellationToken ct)
    {
        Order? order = await orderRepo.GetByIdAsync(id, ct);

        if (order is null)
            return OrderErrors.OrderNotFound(id);

        return order.ToDto();
    }

    public async Task<Result<PaginatedDto<OrderDto>>> GetByCustomerIdAsync(Guid customerId,
                                                                        int page,
                                                                        int pageSize,
                                                                        CancellationToken ct)
    {
        if (!await customerRepo.IsExist(customerId, ct))
            return UserErrors.NotFound(customerId);

        List<Order> orders = await orderRepo.GetByCustomerIdAsync(customerId, page, pageSize, ct);

        int totalCount = await orderRepo.CountAsync(customerId, ct);

        return PaginatedDto<OrderDto>.Create(
            page,
            pageSize,
            orders.ToDto(),
            totalCount);
    }


    public Task<Result<OrderDto>> CheckoutAsync(Guid customerId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}