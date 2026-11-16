using ECommerce.Domain.Entities.Baskets;

namespace ECommerce.Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string basketId);
        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
