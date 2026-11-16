namespace ECommerce.Application.Contracts
{
    public interface ICacheService
    {

        Task<string?> GetDataAsync(string cacheKey);
        Task SetDataAsync(string cacheKey, object cacheValue, TimeSpan? ttl);
    }
}
