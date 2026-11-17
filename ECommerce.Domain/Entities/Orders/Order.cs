using ECommerce.Domain.Entities.Comon;

namespace ECommerce.Domain.Entities.Orders
{
    public class Order : BaseEntity<Guid>
    {
        private Order()
        {

        }
        public Order(string buyerEmail, OrderAddress shipToAddress,
                     ICollection<OrderItem> items,
                     int deliveryMethodId, decimal subTotal, string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            Items = items;
            DeliveryMethodId = deliveryMethodId;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string BuyerEmail { get; set; } = default!;
        public OrderAddress ShipToAddress { get; set; } = default!;
        public ICollection<OrderItem> Items { get; set; } = [];

        public int DeliveryMethodId { get; set; }//FK

        public decimal SubTotal { get; set; }
        //public decimal Total { get; set; } //SubTotal+DeliverMethod


        public DeliveryMethod DeliveryMethod { get; set; } = default!;

        public decimal GetTotal() => SubTotal + (DeliveryMethod?.Price ?? 0);

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;//Failed
        public string PaymentIntentId { get; set; }
        //Not Mapped + get 
    }
}
