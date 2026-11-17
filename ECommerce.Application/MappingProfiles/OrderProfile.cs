using AutoMapper;
using ECommerce.Application.DTOS.Identity;
using ECommerce.Application.DTOS.Orders;
using ECommerce.Domain.Entities.Orders;

namespace ECommerce.Application.MappingProfiles
{
    internal class OrderProfile : Profile

    {

        public OrderProfile()
        {
            CreateMap<AddressDto, OrderAddress>();


            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.DeliveryMethodCost, opt => opt.MapFrom(src => src.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderItemPictureUrlResolver>());


            CreateMap<DeliveryMethod, DeliveryMethodDto>()
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Price));
        }
    }
}
