using GizmoDALV2.Entities;
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
    public class UserAttributeMap : EntityTypeConfiguration<UserAttribute>
    {
        public UserAttributeMap()
        {
            // Primary Key
            this.HasKey(x => x.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("UserAttributeId"); ;

            this.Property(x => x.UserId)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_UserAttribute") { IsUnique = true , Order = 0 }
                }))
                .HasColumnOrder(1);

            this.Property(x => x.AttributeId)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_UserAttribute") { IsUnique = true , Order = 1 }
                }))
                .HasColumnOrder(2);

            this.Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(3);

            // Table & Column Mappings
            this.ToTable(nameof(UserAttribute));

            //Relations
            this.HasRequired(x => x.User)
                .WithMany(x => x.Attributes);
        }
    }
}
