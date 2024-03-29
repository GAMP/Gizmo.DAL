﻿using GizmoDALV2.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class StockTransactionMap : EntityTypeConfiguration<StockTransaction>
    {
        public StockTransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.ToTable(nameof(StockTransaction));

            this.Property(x => x.Id)
                .HasColumnName("StockTransactionId");

            // Relationships        
            this.HasRequired(x => x.Product)
                .WithMany(x => x.StockTransactions)
                .HasForeignKey(x => x.ProductId);

            this.HasOptional(x => x.SourceProduct)
                .WithMany(x=>x.StockTransactionsSource)
                .HasForeignKey(x => x.SourceProductId);

            this.HasOptional(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedById);

            this.HasOptional(x => x.ModifiedBy)
                .WithMany()
                .HasForeignKey(x => x.ModifiedById);
        }
    }
}
