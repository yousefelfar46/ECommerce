using AutoMapper;
using ECommerce.Application.DTOS.Orders;
using ECommerce.Domain.Entities.Orders;
using Microsoft.Extensions.Options;

namespace ECommerce.Application.MappingProfiles
{
    internal class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {

        private readonly UrlSettings _urlSettings;
        public OrderItemPictureUrlResolver(IOptions<UrlSettings> options)
        {
            _urlSettings = options.Value;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            var baseUrl = _urlSettings.BaseUrl;
            var path = source.Product.PictureUrl.TrimStart('/');

            return $"{baseUrl}/Files/{path}";
        }
    }
}
