using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Entities.Base;
using LearningPlatformApi.Persistence.EntitiesConfiguration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration;

public class TestTaskEntityConfiguration : VersionableDbEntityConfiguration<TestTaskEntity, string>
{
    protected override void OverrideConfigure(EntityTypeBuilder<TestTaskEntity> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(x => x.Id);
        entityTypeBuilder.ToTable("TestTask");
    }
}