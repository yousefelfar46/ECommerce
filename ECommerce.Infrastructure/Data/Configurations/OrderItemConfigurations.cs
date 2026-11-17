using ECommerce.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    internal class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(d => d.Price).HasColumnType("decimal(8,2)");

            builder.OwnsOne(oi => oi.Product, product =>
            {
                product.Property(p => p.ProductName).HasMaxLength(100);
                product.Property(p => p.PictureUrl).HasMaxLength(200);
            });

        }
    }
}
