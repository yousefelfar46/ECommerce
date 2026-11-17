using ECommerce.Domain.Entities.Orders;

namespace ECommerce.Application.Specifications.OrderModule
{
    internal class OrderSpecifications : BaseSpecifications<Order, Guid>
    {
        public OrderSpecifications(string email) : base(x => x.BuyerEmail == email)
        {
            AddInclude(x => x.DeliveryMethod);
            AddInclude(x => x.Items);
            AddOrderByDescending(x => x.OrderDate);

        }
        public OrderSpecifications(Guid id, string email) : base(x => x.BuyerEmail == email && x.Id == id)
        {
            AddInclude(x => x.DeliveryMethod);
            AddInclude(x => x.Items);

        }
    }
}
