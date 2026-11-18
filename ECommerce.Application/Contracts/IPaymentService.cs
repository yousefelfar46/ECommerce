using ECommerce.Application.Common;
using ECommerce.Application.DTOS.Baskets;

namespace ECommerce.Application.Contracts
{
    public interface IPaymentService
    {
        Task<Result<BasketDto>> CreateOrUpdatePaymentIntentAsync(string basketId, CancellationToken ct = default);

        Task UpdatePayemntReceivedAsync(string paymentIntentId);
        Task UpdatePayemntFailedAsync(string paymentIntentId);
    }
}
