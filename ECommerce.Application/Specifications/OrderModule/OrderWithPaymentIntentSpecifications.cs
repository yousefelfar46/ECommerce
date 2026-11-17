using ECommerce.Domain.Entities.Orders;

namespace ECommerce.Application.Specifications.OrderModule
{
    public class OrderWithPaymentIntentSpecifications : BaseSpecifications<Order, Guid>
    {
        public OrderWithPaymentIntentSpecifications(string paymentIntentId) : base(o => o.PaymentIntentId == paymentIntentId)
        {

        }
    }
}
