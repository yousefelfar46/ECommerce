using Ecommerce.API.Controllers.BaseController;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTOS.Baskets;
using Microsoft.AspNetCore.Mvc;


namespace Ecommerce.API.Controllers
{

    public class BasketsController : ApiBaseController
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDto>> GetBasket(string id)
        {
            var result = await _basketService.GetBasketAsync(id);
            return ToActionResult(result);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateBasket(BasketDto basketDto)
        {
            var result = await _basketService.CreateOrUpdateBasketAsync(basketDto, null);
            return ToActionResult(result);

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            var result = await _basketService.DeleteBasketAsync(id);
            return ToActionResult(result);
        }

    }
}
