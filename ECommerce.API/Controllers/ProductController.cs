
using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTOS.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{

    public class ProductsController : ApiBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        //GetAll Products
        [HttpGet]//Before Endpoint Check Cache (by cache key ) if found -> return resonse
                 //not found -> enter endpoint -> cache -> return
        [RedisCache(90)]
        [Authorize]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery] ProductQueryParameter parameters, CancellationToken ct)
        {

            var res = await _productService.GetAllProductsAsync(parameters, ct);
            return ToActionResult(res);
        }

        //Get Product by id
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id, CancellationToken ct)
        {
            var res = await _productService.GetProductByIdAsync(id, ct);
            return ToActionResult(res);
        }

        //Get All Types
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetAllTypes(CancellationToken ct)
        {
            var res = await _productService.GetAllTypesAsync(ct);
            return ToActionResult(res);
        }
        //Get All Brands
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllBrand(CancellationToken ct)
        {
            var res = await _productService.GetAllBrandsAsync(ct);
            return ToActionResult(res);
        }
    }
}