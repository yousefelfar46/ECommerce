using AutoMapper;
using ECommerce.Application.DTOS.Baskets;
using ECommerce.Domain.Entities.Baskets;

namespace ECommerce.Application.MappingProfiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}
