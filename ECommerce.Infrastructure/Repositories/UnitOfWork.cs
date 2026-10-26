using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Comon;
using ECommerce.Infrastructure.Data;

namespace ECommerce.Infrastructure.Repositories
{
    internal class UnitOfWork(StoreDbContext _dbContex) : IUnitOfWork
    {

        private readonly Dictionary<string, object> repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if (repositories.TryGetValue(typeName, out object? value))
                return (IGenericRepository<TEntity, TKey>)value;

            else
            {
                var repo = new GenericRepository<TEntity, TKey>(_dbContex);
                repositories[typeName] = repo;

                return repo;
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
       => await _dbContex.SaveChangesAsync(ct);
    }
}
