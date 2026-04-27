using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.EntitiesConfiguration.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration;

public class CourseEntityConfiguration : PublicationDbEntityConfiguration<CourseEntity, string>
{
    protected override void OverrideConfigure(EntityTypeBuilder<CourseEntity> modelBuilder)
    {
        base.OverrideConfigure(modelBuilder);

        modelBuilder.Property(x => x.PageId)
            .IsRequired(false); // Теперь может быть NULL

        modelBuilder.HasMany(x => x.Modules)
            .WithOne()
            .HasForeignKey(e => new { e.CourseId, e.CourseVersion })
            .HasPrincipalKey(e => new { e.Id, e.VersionOrder });
    }
}