    using ECommerce.Domain.Entities.Products;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;
    using System.Collections.Generic;
    using System.Text;

    namespace ECommerce.Infrastructure.Data.Configurations
    {
        public class ProductConfigurations : IEntityTypeConfiguration<Product>
        {
            public void Configure(EntityTypeBuilder<Product> builder)
            {
            builder.HasOne(p => p.ProductBrand)
             .WithMany()
             .HasForeignKey(p => p.BrandId);

            builder.HasOne(p => p.ProductType)
                   .WithMany()
                   .HasForeignKey(p => p.TypeId);

            builder.Property(p => p.Price)
                   .HasColumnType("decimal(10,2)");

            builder.Property(p => p.Name)
                   .HasMaxLength(100);

            builder.Property(p => p.Description)
                   .HasMaxLength(500);

            builder.Property(p => p.PictureUrl)
                   .HasMaxLength(200);

        }
        }
    }
