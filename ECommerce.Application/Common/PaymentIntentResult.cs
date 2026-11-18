namespace ECommerce.Application.Common
{
    public sealed class PaymentIntentResult
    {
        public PaymentIntentResult(string clientSecret, string paymentIntentId)
        {
            ClientSecret = clientSecret;
            PaymentIntentId = paymentIntentId;
        }

        public string ClientSecret { get; set; } = default!;
        public string PaymentIntentId { get; set; } = default!;


    }
}
