using ECommerce.Domain.Entities.Orders;

namespace ECommerce.Application.DTOS.Orders
{
    public class OrderToReturnDto
    {
        public Guid Id { get; set; }
        public string BuyerEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;

        public OrderAddress ShipToAddress { get; set; } = default!;

        public string DeliveryMethod { get; set; } = default!;
        public decimal DeliveryMethodCost { get; set; } = default!;

        public decimal Total { get; set; }
        public decimal SubTotal { get; set; }
        public OrderStatus Status { get; set; } = default!;

        public ICollection<OrderItemDto> Items { get; set; }

    }
}
