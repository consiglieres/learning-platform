using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Entities.Base;
using LearningPlatformApi.Persistence.EntitiesConfiguration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration;

public class ModuleEntityConfiguration : AuditableDbEntityConfiguration<ModuleEntity, string>
{
    protected override void OverrideConfigure(EntityTypeBuilder<ModuleEntity> modelBuilder)
    {
        base.OverrideConfigure(modelBuilder);
        modelBuilder.HasOne<CourseEntity>()
            .WithMany(x => x.Modules)
            .HasForeignKey(e => new { e.CourseId })
            .HasPrincipalKey(e => new { e.Id });

        modelBuilder.HasOne(x => x.Page)
            .WithOne();

        modelBuilder.HasIndex(x => new { x.CourseId, x.ModuleOrder })
            .IsUnique()
            .HasDatabaseName("IX_CourseId_ModuleOrder");
    }
}