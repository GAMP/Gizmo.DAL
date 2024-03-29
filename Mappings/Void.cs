﻿using GizmoDALV2.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class VoidMap : EntityTypeConfiguration<Void>
    {
        public VoidMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .HasColumnName("VoidId")
                .HasColumnOrder(0);

            // Table & Column Mappings
            ToTable(nameof(Void));
        }
    }   
}
