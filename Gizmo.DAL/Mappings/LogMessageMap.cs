using Gizmo.DAL.Entities;

using Gizmo.DAL;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class LogMap : EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(x => x.Time)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("IX_Time")
                }));

            Property(x => x.HostNumber)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("IX_HostNumber")
                })); ;

            Property(t => t.Hostname)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY45);

            Property(x => x.ModuleType)
                .HasColumnOrder(4);

            Property(t => t.ModuleVersion)
                .HasColumnOrder(5)
                .HasMaxLength(SQLStringSize.TINY45);

            Property(x => x.Category)
                .HasColumnOrder(6)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("IX_Category")
                }));

            Property(x => x.MessageType)
                .HasColumnOrder(7)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("IX_MessageType")
                }));

            Property(t => t.Message)
                .IsRequired()
                .HasColumnOrder(8)
                .HasMaxLength(SQLStringSize.NORMAL);

            // Table & Column Mappings
            ToTable("Log");

            Property(t => t.Id)
                .HasColumnName("LogId");
        }
    }

    public class LogExceptionMap : EntityTypeConfiguration<LogException>
    {
        public LogExceptionMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(x => x.ExceptionData)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLByteArraySize.NORMAL);

            // Table & mappings
            ToTable("LogException");

            Property(x => x.Id)
                .HasColumnName("LogId");

            HasRequired(x => x.Message)
                .WithRequiredDependent(x => x.Exception)
                .WillCascadeOnDelete(true);
        }
    }
}
