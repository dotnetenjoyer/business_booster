using BusinessBooster.ToDo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessBooster.ToDo.Infrastructure.DataAccess.EntitiesConfigurations;

/// <summary>
/// Configure the planned task entity.
/// </summary>
public class PlannedTaskConfiguration : IEntityTypeConfiguration<PlannedTask>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<PlannedTask> entity)
    {
        entity.ToTable("tasks");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasColumnType("varchar");

        entity.Property(e => e.Description)
            .IsRequired(false)
            .HasColumnType("varchar");
        
        entity.Property(e => e.CreatedAt)
            .HasColumnType("timestamp")
            .HasDefaultValueSql("(now())");
        
        entity.HasOne(e => e.Plan)
            .WithMany(p => p.Tasks)
            .HasForeignKey(e => e.PlanId);

        entity.HasMany(e => e.StatusHistory)
            .WithOne(r => r.Task)
            .HasForeignKey(r => r.TaskId);
    }
}