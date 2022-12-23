using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLib;
using System;

namespace GizmoDALV2.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Indexes
            builder.HasIndex(t => t.Id);

            builder.ToTable("Product");

            builder.HasData(new Product()
            {
                Id = 1,
                Name = "Mars Bar",
                Cost = 0.90m,
                Price = 1.10m,
                Points = 10,
                StockOptions = StockOptionType.EnableStock,
                ProductGroupId = 4,
                Guid = new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba1"),
            });

            builder.HasData(new Product()
            {
                Id = 2,
                Name = "Snickers Bar",
                Points = 15,
                StockOptions = StockOptionType.EnableStock,
                Cost = 1.20m,
                Price = 2.0m,
                ProductGroupId = 4,
                Guid = new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba2"),
            });

            builder.HasData(new Product()
            {
                Id = 3,
                Name = "Pizza (Small)",
                ProductGroupId = 2,
                Cost = 2.20m,
                Price = 6.0m,
                Guid = new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba3"),
            });

            builder.HasData(new Product()
            {
                Id = 4,
                Name = "Coca Cola (Can)",
                ProductGroupId = 3,
                Cost = 1.20m,
                Price = 2.0m,
                Guid = new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba4"),
            });
        }        
    }
}
