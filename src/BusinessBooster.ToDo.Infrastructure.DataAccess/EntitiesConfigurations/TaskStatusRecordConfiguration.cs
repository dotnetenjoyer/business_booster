using BusinessBooster.ToDo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessBooster.ToDo.Infrastructure.DataAccess.EntitiesConfigurations;

/// <summary>
/// Configure task status record entity.
/// </summary>
public class TaskStatusRecordConfiguration : IEntityTypeConfiguration<TaskStatusRecord>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<TaskStatusRecord> entity)
    {
        entity.ToTable("tasks_status_records");

        entity.HasKey(e => new { e.TaskId, e.CreatedAt });

        entity.Property(e => e.CreatedAt)
            .HasColumnType("timestamp")
            .HasDefaultValueSql("(now())");

        entity.Property(e => e.Status)
            .HasColumnType("int");
        
        entity.HasOne(e => e.Task)
            .WithMany()
            .HasForeignKey(e => e.TaskId);
    }
}