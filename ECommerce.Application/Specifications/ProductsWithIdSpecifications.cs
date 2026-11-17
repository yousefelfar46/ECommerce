using ECommerce.Domain.Entities.Products;

namespace ECommerce.Application.Specifications
{
    internal class ProductsWithIdSpecifications : BaseSpecifications<Product, int>
    {

        public ProductsWithIdSpecifications(HashSet<int> productIds)
            : base(p => productIds.Contains(p.Id))
        {

        }
    }
}
