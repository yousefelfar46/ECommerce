using ECommerce.Application.Common;
using ECommerce.Application.DTOS.Baskets;

namespace ECommerce.Application.Contracts
{
    public interface IBasketService
    {
        Task<Result<BasketDto>> GetBasketAsync(string basketId);
        Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket, TimeSpan? ttl);
        Task<Result<bool>> DeleteBasketAsync(string basketId);


    }
}
