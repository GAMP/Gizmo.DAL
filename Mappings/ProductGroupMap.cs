using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class ProductGroupMap : EntityTypeConfiguration<ProductGroup>
    {
        public ProductGroupMap()
        {
            // Key
            HasKey(x => x.Id);

            //Properties
            Property(x => x.Name)
                .HasMaxLength(SQLStringSize.TINY45)
                .IsRequired()
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true } 
                }));

            // Relations
            ToTable("ProductGroup");

            Property(x => x.Id)
                .HasColumnName("ProductGroupId");

            HasMany(x => x.ChildGroups)
                .WithOptional(x => x.Parent)
                .HasForeignKey(x => x.ParentId);
        }
    }
}
