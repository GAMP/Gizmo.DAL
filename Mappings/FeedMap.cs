﻿using GizmoDALV2.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class FeedMap : EntityTypeConfiguration<Feed>
    {
        public FeedMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnOrder(0);

            this.Property(t => t.Title)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(t => t.Url)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(x => x.Maximum)
                .HasColumnOrder(3);

            // Table & Column Mappings
            this.ToTable("Feed");

            this.Property(t => t.Id)
                .HasColumnName("FeedId");
        }
    }
}
