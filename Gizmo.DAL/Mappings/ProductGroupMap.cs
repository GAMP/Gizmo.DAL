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
    public class ProductGroupMap : EntityTypeConfiguration<ProductGroup>
    {
        public ProductGroupMap()
        {
            // Key
            this.HasKey(x => x.Id);

            //Properties
            this.Property(x => x.Name)
                .HasMaxLength(SQLStringSize.TINY45)
                .IsRequired()
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true } 
                }));

            // Relations
            this.ToTable("ProductGroup");

            this.Property(x => x.Id)
                .HasColumnName("ProductGroupId");

            this.HasMany(x => x.ChildGroups)
                .WithOptional(x => x.Parent)
                .HasForeignKey(x => x.ParentId);
        }
    }
}
