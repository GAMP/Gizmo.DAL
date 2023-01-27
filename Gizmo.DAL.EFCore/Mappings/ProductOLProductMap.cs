﻿using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class ProductOLProductMap : IEntityTypeConfiguration<ProductOLProduct>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductOLProduct> builder)
        {
            builder.ToTable(nameof(ProductOLProduct));

            builder.HasOne(x => x.Product)
                .WithMany(x => x.OrderLines)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}