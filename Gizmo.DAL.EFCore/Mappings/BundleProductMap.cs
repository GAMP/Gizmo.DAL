﻿using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class BundleProductMap : IEntityTypeConfiguration<BundleProduct>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<BundleProduct> builder)
        {
            // Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("BundleProductId");

            builder.Property(x => x.ProductBundleId)
                .HasColumnOrder(1);

            builder.Property(x => x.ProductId)
                .HasColumnOrder(2);

            builder.Property(x => x.Quantity)
                .HasColumnOrder(3);
            
            // Relations
            builder.ToTable("BundleProduct");

            builder.HasOne(x => x.ProductBundle)
                .WithMany(x => x.BundledProducts)
                .HasForeignKey(x => x.ProductBundleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId);

            //Seeds
            builder.HasData(new BundleProduct() { Id = 1, ProductBundleId = 5, Price = 1, ProductId = 3, Quantity = 1 });
            builder.HasData(new BundleProduct() { Id = 2, ProductBundleId = 5, Price = 2, ProductId = 4, Quantity = 1 });
        }
    }
}