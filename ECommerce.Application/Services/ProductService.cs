using AutoMapper;
using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTOS.Products;
using ECommerce.Application.Specifications;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Products;

namespace ECommerce.Application.Services
{
    internal class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedResult<ProductDto>>> GetAllProductsAsync(ProductQueryParameter parameters, CancellationToken ct = default)
        {

            var prodSpecs = new ProductWithTypeAndBrandSpecs(parameters);
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(prodSpecs, ct);


            var data = _mapper.Map<IReadOnlyList<ProductDto>>(products);
            var countOfSpecsProd = await _unitOfWork.GetRepository<Product, int>().CountAsync(new ProductCountSpecifications(parameters), ct);


            var result = new PaginatedResult<ProductDto>(parameters.PageIndex, parameters.PageSize, countOfSpecsProd, data);

            return Result<PaginatedResult<ProductDto>>.Ok(result);
        }





        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct = default)
        {


            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            //Mapping
            var mappedBrands = _mapper.Map<IReadOnlyList<BrandDto>>(brands);


            return Result<IReadOnlyList<BrandDto>>.Ok(mappedBrands);
        }



        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct = default)
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            //Mapping
            var mappedTypes = _mapper.Map<IReadOnlyList<TypeDto>>(types);

            return Result<IReadOnlyList<TypeDto>>.Ok(mappedTypes);
        }

        public async Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct = default)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(new ProductWithTypeAndBrandSpecs(id), ct);

            if (product == null) return Error.NotFound("Product not found", $"Product With Id {id} not found");
            return _mapper.Map<ProductDto>(product);

        }
    }
}
