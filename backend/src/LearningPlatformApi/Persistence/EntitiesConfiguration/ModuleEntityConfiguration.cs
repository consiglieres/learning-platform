using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.EntitiesConfiguration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration;

public class ModuleEntityConfiguration : VersionableDbEntityConfiguration<ModuleEntity, string>
{
    protected override void OverrideConfigure(EntityTypeBuilder<ModuleEntity> modelBuilder)
    {
        base.OverrideConfigure(modelBuilder);

        modelBuilder.HasIndex(x => new { x.CourseId, x.ModuleOrder })
            .IsUnique()
            .HasDatabaseName("IX_CourseId_ModuleOrder");
    }
}