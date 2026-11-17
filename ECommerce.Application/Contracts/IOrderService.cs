using ECommerce.Application.Common;
using ECommerce.Application.DTOS.Orders;

namespace ECommerce.Application.Contracts
{
    public interface IOrderService
    {
        Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto, string email, CancellationToken ct = default);

        Task<Result<IReadOnlyList<OrderToReturnDto>>> GetAllOrdersForUserAsync(string email, CancellationToken ct = default);
        Task<Result<OrderToReturnDto>> GetOrderByIdAndEmailForUserAsync(Guid id, string email, CancellationToken ct = default);


        Task<Result<IReadOnlyList<DeliveryMethodDto>>> GetDeliveryMethodAsync(CancellationToken ct = default);
    }
}
