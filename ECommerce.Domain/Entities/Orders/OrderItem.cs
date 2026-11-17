using ECommerce.Domain.Entities.Comon;

namespace ECommerce.Domain.Entities.Orders
{
    public class OrderItem : BaseEntity<int>
    {

        public ProductItemOrdered Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
