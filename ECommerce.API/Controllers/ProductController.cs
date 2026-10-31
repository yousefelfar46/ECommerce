
using Ecommerce.API.Controllers.BaseController;
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


//{
//    "Id": 1,
//            "Name": "Classic White T-Shirt",
//            "Description": "A soft cotton white t-shirt that goes with everything. Perfect for casual everyday wear.",
//            "PictureUrl": "http://localhost:5213/Files/images/products/ClassicWhiteTShirt.jpeg",
//            "Price": 120.00,
//            "ProductBrand": "H&M",
//            "ProductType": "Tops"
//        },


//     {
//    "id": 8,
//            "name": "Casual Sneakers",
//            "description": "Lightweight sneakers designed for everyday wear. Breathable material with cushioned soles.",
//            "pictureUrl": "http://localhost:5213/Files/images/products/CasualSneakers.jpg",
//            "price": 600.00,
//            "productBrand": "Nike",
//            "productType": "Accessories & Footwear"
//        },