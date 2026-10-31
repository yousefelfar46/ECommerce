using ECommerce.Application.Common;
using ECommerce.Domain.Entities.Products;

namespace ECommerce.Application.Specifications
{
    internal class ProductWithTypeAndBrandSpecs : BaseSpecifications<Product, int>
    {

        //Get All 
        public ProductWithTypeAndBrandSpecs() : base(null)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

        }
           
        //Get By Id
        public ProductWithTypeAndBrandSpecs(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }

    }
}
