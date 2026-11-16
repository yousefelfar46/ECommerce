using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Baskets;
using StackExchange.Redis;
using System.Text.Json;

namespace ECommerce.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {

        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive)
        {
            var value = JsonSerializer.Serialize(basket);
            var res = await _database.StringSetAsync(basket.Id, value, timeToLive ?? TimeSpan.FromDays(7));
            return res ? basket : null;
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);
            if (basket.IsNullOrEmpty) return null;
            return JsonSerializer.Deserialize<CustomerBasket>(basket.ToString());
        }
    }
}
