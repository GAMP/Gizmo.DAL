using GizmoDALV2.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class PaymentMethodMap : EntityTypeConfiguration<PaymentMethod>
    {
        public PaymentMethodMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasColumnName("PaymentMethodId")
                .HasColumnOrder(0);

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true } 
                }));

            Property(x => x.Description)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(2);

            Property(x => x.Surcharge)
                .HasColumnOrder(3);

            Property(x => x.DisplayOrder)
                .HasColumnOrder(4);

            Property(x => x.IsEnabled)
                .HasColumnOrder(5);

            Property(x => x.Options)
                .HasColumnOrder(6);

            // Table & Column Mappings
            ToTable("PaymentMethod");              
        }
    }
}
