using BusinessBooster.ToDo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessBooster.ToDo.Infrastructure.DataAccess.EntitiesConfigurations;

/// <summary>
/// Configure the plan entity.
/// </summary>
public class PlanEntityConfiguration : IEntityTypeConfiguration<Plan>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Plan> entity)
    {
        entity.ToTable("plans");

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
        
        entity.Property(e => e.RemovedAt)
            .HasColumnType("timestamp")
            .HasComment("For soft-deletes");

        entity.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId);

        entity.HasMany(e => e.Tasks)
            .WithOne(t => t.Plan)
            .HasForeignKey(x => x.PlanId);
        
        entity.HasQueryFilter(e => e.RemovedAt == null);
    }
}