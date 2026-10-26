using AutoMapper;
using ECommerce.Application.DTOS.Products;
using ECommerce.Domain.Entities.Products;
using Microsoft.Extensions.Options;

namespace ECommerce.Application.MappingProfiles
{
    internal class PictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly UrlSettings _urlSettings;
        public PictureUrlResolver(IOptions<UrlSettings> options)
        {
            _urlSettings = options.Value;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            //src => images/products/formalblazer.png
            //return http://localhost:5213/api/Files/images/products/formalblazer.png


            var baseUrl = _urlSettings.BaseUrl;
            var path = source.PictureUrl.TrimStart('/');

            return $"{baseUrl}/Files/{path}";
        }
    }

    public class UrlSettings
    {
        public string BaseUrl { get; set; }
    }
}
