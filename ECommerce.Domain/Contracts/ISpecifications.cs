using ECommerce.Domain.Entities.Comon;
using System.Linq.Expressions;

namespace ECommerce.Domain.Contracts
{
    public interface ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
        Expression<Func<TEntity, bool>> Criteria { get; }

        Expression<Func<TEntity, object>>? OrderBy { get; }
        Expression<Func<TEntity, object>>? OrderByDescending { get; }

        public int Take { get; }
        public int Skip { get; }

        public bool IsPaginated { get; }


    }
}
