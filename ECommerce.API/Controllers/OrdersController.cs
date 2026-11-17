using Ecommerce.API.Controllers.BaseController;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTOS.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{

    public class OrdersController : ApiBaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto, CancellationToken ct)
        {
            return ToActionResult(await _orderService.CreateOrderAsync(orderDto, GetEmailFromToken(), ct));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetAllOrders(CancellationToken ct)
        {
            return ToActionResult(await _orderService.GetAllOrdersForUserAsync(GetEmailFromToken(), ct));
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id, CancellationToken ct)
        {
            return ToActionResult(await _orderService.GetOrderByIdAndEmailForUserAsync(id, GetEmailFromToken(), ct));
        }

        [HttpGet("deliverymethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethodDto>>> GetDeliveryMethods(CancellationToken ct)
        {
            return ToActionResult(await _orderService.GetDeliveryMethodAsync(ct));
        }

    }
}
