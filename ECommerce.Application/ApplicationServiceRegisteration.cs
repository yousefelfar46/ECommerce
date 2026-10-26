using ECommerce.Application.Contracts;
using ECommerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application
{
    public static class ApplicationServiceRegisteration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(c => { }, typeof(ApplicationServiceRegisteration).Assembly);
            services.AddScoped<IProductService, ProductService>();
           
            return services;
        }
    }
}
