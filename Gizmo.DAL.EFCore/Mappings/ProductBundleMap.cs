using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLib;
using System;
using System.Reflection.Metadata;

namespace GizmoDALV2.Mappings
{
    public class ProductBundleMap : IEntityTypeConfiguration<ProductBundle>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductBundle> builder)
        {
            // Indexes
            builder.HasIndex(t => t.Id);

            // Table
            builder.ToTable("ProductBundle");

            //Seeds
            builder.HasData(new ProductBundle()
            {
                Id = 5,
                Name = "Pizza and Cola",
                StockOptions = StockOptionType.EnableStock,
                Points = 200,
                Price = 3.40m, //pizza plus cola
                ProductGroupId = 2,
                Guid = new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba5"),
            });
        }
    }
}