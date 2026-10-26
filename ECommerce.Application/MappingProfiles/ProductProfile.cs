using AutoMapper;
using ECommerce.Application.DTOS.Products;
using ECommerce.Domain.Entities.Products;

namespace ECommerce.Application.MappingProfiles
{
    internal class ProductProfile : Profile
    {

        public ProductProfile()
        {
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.Name))
               .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<PictureUrlResolver>());

        }
    }
}
