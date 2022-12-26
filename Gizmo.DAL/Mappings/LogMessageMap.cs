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
    public class LogMap : EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnOrder(0);

            this.Property(x => x.Time)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("IX_Time") 
                }));

            this.Property(x => x.HostNumber)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("IX_HostNumber") 
                })); ;

            this.Property(t => t.Hostname)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY45);

            this.Property(x => x.ModuleType)
                .HasColumnOrder(4);

            this.Property(t => t.ModuleVersion)
                .HasColumnOrder(5)
                .HasMaxLength(SQLStringSize.TINY45);

            this.Property(x => x.Category)
                .HasColumnOrder(6)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("IX_Category") 
                }));

            this.Property(x => x.MessageType)
                .HasColumnOrder(7)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("IX_MessageType") 
                }));

            this.Property(t => t.Message)
                .IsRequired()
                .HasColumnOrder(8)
                .HasMaxLength(SQLStringSize.NORMAL);

            // Table & Column Mappings
            this.ToTable("Log");

            this.Property(t => t.Id)
                .HasColumnName("LogId");
        }
    }

    public class LogExceptionMap : EntityTypeConfiguration<LogException>
    {
        public LogExceptionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(x => x.ExceptionData)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLByteArraySize.NORMAL);

            // Table & mappings
            this.ToTable("LogException");

            this.Property(x => x.Id)
                .HasColumnName("LogId");

            this.HasRequired(x => x.Message)
                .WithRequiredDependent(x => x.Exception)
                .WillCascadeOnDelete(true);
        }
    }
}
