using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Comon;
using ECommerce.Domain.Entities.Products;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ECommerce.Infrastructure.DataSeeding
{
    internal class CatalogDataSeeder(StoreDbContext _dbContext, ILogger<CatalogDataSeeder> _logger) : IDataSeeder
    {

        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {

                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync(ct);
                if (pendingMigrations.Any())
                    await _dbContext.Database.MigrateAsync(ct);


                var seedRoot = Path.Combine(AppContext.BaseDirectory, "DataSeed");


                await SeedIfEmptyAsync<ProductBrand, int>(seedRoot, "brands.json", ct);
                await SeedIfEmptyAsync<ProductType, int>(seedRoot, "types.json", ct);
                await SeedIfEmptyAsync<Product, int>(seedRoot, "products.json", ct);


                int res = await _dbContext.SaveChangesAsync(ct);
                if (res > 0)
                {
                    _logger.LogInformation($"Data seeding completed successfully. {res} records added.");
                }
                else
                {
                    _logger.LogInformation("No new data was added during seeding.");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private async Task SeedIfEmptyAsync<T, TKey>(string seedRoot, string fileName, CancellationToken ct) where T : BaseEntity<TKey>
        {


            if (await _dbContext.Set<T>().AnyAsync())
            {
                _logger.LogInformation("Data already exists. Skipping seeding.");
                return;
            }

            var path = Path.Combine(seedRoot, fileName);

            if (!File.Exists(path))
            {
                _logger.LogWarning($"Seed file not found: {path}");
                return;
            }

            using var fileStream = File.OpenRead(path);

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };


            var items = await JsonSerializer.DeserializeAsync<List<T>>(fileStream, options, ct);

            if (items?.Any() ?? false)
                _dbContext.Set<T>().AddRange(items);



        }
    }
}
