using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class RegisterMap : EntityTypeConfiguration<Register>
    {
        public RegisterMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnName("RegisterId")
                .HasColumnOrder(0);

            this.Property(x => x.Number)
                .HasColumnOrder(1);

            this.Property(t => t.Name)
                .IsRequired()
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY45);

            this.Property(t => t.MacAddress)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnAnnotation(
                "Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_MACAddress") { IsUnique = true }
                }));

            this.Property(x => x.StartCash)
                .HasColumnOrder(4);

            this.Property(x => x.IdleTimeout)
                .HasColumnOrder(5);

            this.Property(x => x.Options)
                .HasColumnOrder(6);

            this.Property(t => t.IsDeleted)
                .HasColumnOrder(7);

            // Table & Column Mappings
            this.ToTable(nameof(Register));

            this.HasMany(x => x.Shifts)
                .WithRequired(x => x.Register)
                .HasForeignKey(x => x.RegisterId);

            this.HasMany(x => x.Transactions)
                .WithRequired(x => x.Register)
                .HasForeignKey(x => x.RegisterId);
        }
    }
}
