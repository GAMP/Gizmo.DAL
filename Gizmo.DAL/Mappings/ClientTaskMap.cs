using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class ClientTaskMap : IEntityTypeConfiguration<ClientTask>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ClientTask> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("ClientTaskId");

            // Table & Column Mappings
            builder.HasOne(x => x.TaskBase)
                .WithMany(x=>x.UsedByTask)
                .HasForeignKey(x => x.TaskBaseId);

            builder.ToTable("ClientTask");
        }
    }
}
