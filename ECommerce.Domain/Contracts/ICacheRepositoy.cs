namespace ECommerce.Domain.Contracts
{
    public interface ICacheRepositoy
    {
        Task<string?> GetAsync(string cacheKey);
        Task SetAsync(string cacheKey, string chacheValue, TimeSpan? ttl);
    }
}
