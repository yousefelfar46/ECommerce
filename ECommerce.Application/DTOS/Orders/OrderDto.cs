using ECommerce.Application.DTOS.Identity;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOS.Orders
{
    public class OrderDto
    {

        [Required]
        public string BasketId { get; set; } = default!;
        [Required]
        public int DeliveryMethodId { get; set; }
        [Required]
        public AddressDto ShipToAddress { get; set; } = default!;

    }
}
