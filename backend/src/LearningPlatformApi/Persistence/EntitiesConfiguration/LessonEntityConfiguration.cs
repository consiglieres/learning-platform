using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.EntitiesConfiguration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration;

public class LessonEntityConfiguration : AuditableDbEntityConfiguration<LessonEntity, string>
{
    protected override void OverrideConfigure(EntityTypeBuilder<LessonEntity> modelBuilder)
    {
        base.OverrideConfigure(modelBuilder);

        modelBuilder.HasIndex(x => new { x.ModuleId, x.LessonOrder })
            .IsUnique()
            .HasDatabaseName("IX_ModuleId_LessonOrder");

        modelBuilder.HasMany(x => x.Tasks)
            .WithOne(x => x.Lesson)
            .HasForeignKey(e => new { e.LessonId })
            .HasPrincipalKey(e => new { e.Id });
    }
}