﻿using GizmoDALV2;
using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class RegisterMap : IEntityTypeConfiguration<Register>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Register> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("RegisterId")
                .HasColumnOrder(0);

            builder.Property(x => x.Number)
                .HasColumnOrder(1);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(t => t.MacAddress)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(x => x.StartCash)
                .HasColumnOrder(4);

            builder.Property(x => x.IdleTimeout)
                .HasColumnOrder(5);

            builder.Property(x => x.Options)
                .HasColumnOrder(6);

            builder.Property(t => t.IsDeleted)
                .HasColumnOrder(7);

            // Indexes
            builder.HasIndex(t => t.MacAddress).HasDatabaseName("UQ_MACAddress").IsUnique();

            // Table & Column Mappings
            builder.ToTable(nameof(Register));

            builder.HasMany(x => x.Shifts)
                .WithOne(x => x.Register)
                .HasForeignKey(x => x.RegisterId);

            builder.HasMany(x => x.Transactions)
                .WithOne(x => x.Register)
                .HasForeignKey(x => x.RegisterId);            
        }
    }
}