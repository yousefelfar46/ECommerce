using ECommerce.Domain.Contracts;

namespace ECommerce.API.Extensions
{
    public static class WebApplicationsExtensions
    {

        public static async Task<WebApplication> SeedAndMigrateDataAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dataSeeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");
           //var identitySeeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Identity");
            await dataSeeder.SeedDataAsync();
         //   await identitySeeder.SeedDataAsync();


            return app;
        }
    }
}
