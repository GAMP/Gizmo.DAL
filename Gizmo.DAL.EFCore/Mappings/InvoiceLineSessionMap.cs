﻿using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class InvoiceLineSessionMap : IEntityTypeConfiguration<InvoiceLineSession>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<InvoiceLineSession> builder)
        {
            builder.ToTable(nameof(InvoiceLineSession));

            // Indexes
            builder.HasIndex(t => t.UsageSessionId).IsUnique().HasFilter(null);
            builder.HasIndex(t => t.Id);

            builder.HasOne(x => x.OrderLine)
                .WithMany()
                .HasForeignKey(x => x.OrderLineId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.UsageSession)
                .WithMany(x=>x.InvoiceLines)
                .HasForeignKey(x => x.UsageSessionId);
        }
    }
}
