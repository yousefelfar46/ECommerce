using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Comon;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories
{
    internal class GenericRepository<TEntity, TKey>(StoreDbContext _dbContext)
        : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public void Add(TEntity entity) => _dbContext.Set<TEntity>().Add(entity);

        public Task<int> CountAsync(ISpecifications<TEntity, TKey> specs, CancellationToken ct = default)
        {
            return SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specs).CountAsync(ct);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
       => await _dbContext.Set<TEntity>().ToListAsync(ct);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specs, CancellationToken ct = default)
        {

            var query = SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specs);

            return await query.ToListAsync(ct);
        }

        public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default)
       => await _dbContext.Set<TEntity>().FindAsync(id, ct);

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specs, CancellationToken ct = default)
        {

            var query = SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specs);
            return await query.FirstOrDefaultAsync(ct);
        }

        public void Remove(TEntity entity)
      => _dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)
        => _dbContext.Set<TEntity>().Update(entity);
    }
}
