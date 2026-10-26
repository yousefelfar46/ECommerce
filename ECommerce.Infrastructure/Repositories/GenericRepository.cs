using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Comon;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories
{
    internal class GenericRepository<TEntity, TKey>(StoreDbContext _dbContext)
        : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public void Add(TEntity entity) => _dbContext.Set<TEntity>().Add(entity);

    

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
       => await _dbContext.Set<TEntity>().ToListAsync(ct);

     

        public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default)
       => await _dbContext.Set<TEntity>().FindAsync(id, ct);

    

        public void Remove(TEntity entity)
      => _dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)
        => _dbContext.Set<TEntity>().Update(entity);
    }
}
