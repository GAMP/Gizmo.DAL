using Gizmo.DAL.Entities;

using Gizmo.DAL;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class UserAttributeMap : EntityTypeConfiguration<UserAttribute>
    {
        public UserAttributeMap()
        {
            // Primary Key
            HasKey(x => x.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("UserAttributeId"); ;

            Property(x => x.UserId)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_UserAttribute") { IsUnique = true , Order = 0 }
                }))
                .HasColumnOrder(1);

            Property(x => x.AttributeId)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_UserAttribute") { IsUnique = true , Order = 1 }
                }))
                .HasColumnOrder(2);

            Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(3);

            // Table & Column Mappings
            ToTable(nameof(UserAttribute));

            //Relations
            HasRequired(x => x.User)
                .WithMany(x => x.Attributes);
        }
    }
}
