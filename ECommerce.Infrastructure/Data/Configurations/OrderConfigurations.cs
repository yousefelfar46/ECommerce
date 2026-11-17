using ECommerce.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(o => o.Items)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(o => o.SubTotal).HasColumnType("decimal(8,2)");


            builder.OwnsOne(o => o.ShipToAddress, address =>
            {
                address.Property(a => a.Country).HasMaxLength(50);
                address.Property(a => a.City).HasMaxLength(50);
                address.Property(a => a.Street).HasMaxLength(50);
                address.Property(a => a.FirstName).HasMaxLength(50);
                address.Property(a => a.LastName).HasMaxLength(50);
            });
            builder.Property(o => o.Status).HasConversion<string>().HasMaxLength(50);
        }
    }
}
