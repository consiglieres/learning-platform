using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.EntitiesConfiguration.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration;

public class TestTaskEntityConfiguration : VersionableDbEntityConfiguration<TestTaskEntity, string>
{
    protected override void OverrideConfigure(EntityTypeBuilder<TestTaskEntity> modelBuilder)
    {
        modelBuilder.HasKey(x => x.Id);
    }
}