using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class RegisterMap : EntityTypeConfiguration<Register>
    {
        public RegisterMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnName("RegisterId")
                .HasColumnOrder(0);

            Property(x => x.Number)
                .HasColumnOrder(1);

            Property(t => t.Name)
                .IsRequired()
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY45);

            Property(t => t.MacAddress)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnAnnotation(
                "Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_MACAddress") { IsUnique = true }
                }));

            Property(x => x.StartCash)
                .HasColumnOrder(4);

            Property(x => x.IdleTimeout)
                .HasColumnOrder(5);

            Property(x => x.Options)
                .HasColumnOrder(6);

            Property(t => t.IsDeleted)
                .HasColumnOrder(7);

            // Table & Column Mappings
            ToTable(nameof(Register));

            HasMany(x => x.Shifts)
                .WithRequired(x => x.Register)
                .HasForeignKey(x => x.RegisterId);

            HasMany(x => x.Transactions)
                .WithRequired(x => x.Register)
                .HasForeignKey(x => x.RegisterId);            
        }
    }
}
