using ECommerce.Application.Common;

namespace ECommerce.Application.Contracts
{
    public interface IPaymentGateway
    {
        //Create Payment Intent
        Task<PaymentIntentResult> CreatePaymentIntentAsync(decimal amount, string currency, CancellationToken ct = default);
        //Update Payment Intent
        Task<PaymentIntentResult> UpdatePaymentIntentAsync(decimal amount, string paymentIntentId, CancellationToken ct = default);

    }
}
