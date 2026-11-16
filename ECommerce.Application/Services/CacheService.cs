using ECommerce.Application.Contracts;
using ECommerce.Domain.Contracts;
using System.Text.Json;

namespace ECommerce.Application.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepositoy _cacheRepositoy;

        public CacheService(ICacheRepositoy cacheRepositoy)
        {
            _cacheRepositoy = cacheRepositoy;
        }
        public async Task<string?> GetDataAsync(string cacheKey)
      => await _cacheRepositoy.GetAsync(cacheKey);

        public async Task SetDataAsync(string cacheKey, object cacheValue, TimeSpan? ttl)
        {
            var jsonValue = JsonSerializer.Serialize(cacheValue, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            await _cacheRepositoy.SetAsync(cacheKey, jsonValue, ttl);
        }
    }
}
