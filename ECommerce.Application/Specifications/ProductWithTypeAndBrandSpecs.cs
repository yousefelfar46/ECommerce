using ECommerce.Application.Common;
using ECommerce.Domain.Entities.Products;

namespace ECommerce.Application.Specifications
{
    internal class ProductWithTypeAndBrandSpecs : BaseSpecifications<Product, int>
    {

        //Get All 
        public ProductWithTypeAndBrandSpecs(ProductQueryParameter parameters)
            : base(p => (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId.Value)
            && (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId.Value)
            && (string.IsNullOrWhiteSpace(parameters.SearchValue) || p.Name.ToLower().Contains(parameters.SearchValue.ToLower()))
            )
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);


            switch (parameters.Sort)
            {

                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Id);
                    break;

            }

            ApplyPagination(parameters.PageSize, parameters.PageIndex);
        }
        //Get By Id
        public ProductWithTypeAndBrandSpecs(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }

    }
}
