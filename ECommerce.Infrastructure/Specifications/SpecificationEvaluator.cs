using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Comon;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Specifications
{
    internal static class SpecificationEvaluator
    {


        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> specs)
            where TEntity : BaseEntity<TKey>
        {
            var baseQuery = inputQuery;


            if (specs.Criteria != null)
            {
                baseQuery = baseQuery.Where(specs.Criteria);
            }

            if (specs.IncludeExpressions != null && specs.IncludeExpressions.Any())
            {

                baseQuery = specs.IncludeExpressions.Aggregate(baseQuery, (current, next) => current.Include(next));

            }

            if (specs.OrderBy != null)
            {
                baseQuery = baseQuery.OrderBy(specs.OrderBy);
            }
            else if (specs.OrderByDescending != null)
            {
                baseQuery = baseQuery.OrderByDescending(specs.OrderByDescending);
            }


            if (specs.IsPaginated)
            {
                baseQuery = baseQuery.Skip(specs.Skip).Take(specs.Take);
            }

            return baseQuery;


        }
    }

}
