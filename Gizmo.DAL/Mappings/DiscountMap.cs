using Gizmo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class DiscountBaseMap : EntityTypeConfiguration<DiscountBase>
    {
        public DiscountBaseMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasColumnOrder(0)
                .HasColumnName("DiscountId");

            this.Property(x => x.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true } 
                }));

            this.Property(x => x.Value)
                .HasColumnOrder(2);

            this.Property(x => x.DiscountAmountType)
                .HasColumnOrder(3);            

            // Table & Column Mappings
            this.ToTable("DiscountBase");

            // Relationships

        }
    }

    public class DiscountUserMap : EntityTypeConfiguration<DiscountUser>
    {
        public DiscountUserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasColumnOrder(0)
                .HasColumnName("DiscountUserId");  

            // Table & Column Mappings
            this.ToTable("DiscountUser");

            // Relationships

            this.HasRequired(x => x.UserGroup)
                .WithMany()
                .WillCascadeOnDelete(true);

            this.HasRequired(x => x.Discount)
                .WithMany()
                .WillCascadeOnDelete(true);

        }
    }

    public class DiscountProductMap : EntityTypeConfiguration<DiscountProduct>
    {
        public DiscountProductMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasColumnOrder(0)
                .HasColumnName("DiscountProductId");

            // Table & Column Mappings
            this.ToTable("DiscountUser");

            // Relationships

            this.HasRequired(x => x.Product)
                .WithMany()
                .WillCascadeOnDelete(true);

            this.HasRequired(x => x.Discount)
                .WithMany()
                .WillCascadeOnDelete(true);

        }
    }

    public class DiscountProductGroupMap : EntityTypeConfiguration<DiscountProductGroup>
    {
        public DiscountProductGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasColumnOrder(0)
                .HasColumnName("DiscountProductGroupId");

            // Table & Column Mappings
            this.ToTable("DiscountUser");

            // Relationships

            this.HasRequired(x => x.ProductGroup)
                .WithMany()
                .WillCascadeOnDelete(true);

            this.HasRequired(x => x.Discount)
                .WithMany()
                .WillCascadeOnDelete(true);

        }
    }
}
