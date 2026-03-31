using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.EntitiesConfiguration.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration;

public class LessonEntityConfiguration : VersionableDbEntityConfiguration<LessonEntity, string>
{
    protected override void OverrideConfigure(EntityTypeBuilder<LessonEntity> modelBuilder)
    {
        modelBuilder.HasKey(x => x.Id);
    }
}