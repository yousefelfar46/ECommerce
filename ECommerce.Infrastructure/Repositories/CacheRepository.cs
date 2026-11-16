using ECommerce.Domain.Contracts;
using StackExchange.Redis;

namespace ECommerce.Infrastructure.Repositories
{
    public class CacheRepository : ICacheRepositoy
    {
        private readonly IDatabase _database;
        public CacheRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }
        public async Task<string?> GetAsync(string cacheKey)
        {
            var value = await _database.StringGetAsync(cacheKey);
            return value.IsNullOrEmpty ? null : value.ToString();
        }

        public async Task SetAsync(string cacheKey, string chacheValue, TimeSpan? ttl)
        {
            await _database.StringSetAsync(cacheKey, chacheValue, ttl ?? TimeSpan.FromDays(2));
        }
    }
}
