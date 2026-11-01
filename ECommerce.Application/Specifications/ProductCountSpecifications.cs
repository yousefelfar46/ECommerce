using ECommerce.Application.Common;
using ECommerce.Domain.Entities.Products;

namespace ECommerce.Application.Specifications
{
    internal class ProductCountSpecifications : BaseSpecifications<Product, int>
    {

        public ProductCountSpecifications(ProductQueryParameter parameters)
       : base(p => (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId.Value)
       && (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId.Value)
       && (string.IsNullOrWhiteSpace(parameters.SearchValue) || p.Name.ToLower().Contains(parameters.SearchValue.ToLower()))
       )
        {

        }
    }
}
