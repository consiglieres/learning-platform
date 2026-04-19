using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.EntitiesConfiguration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration;

public class CodingTaskEntityConfiguration : VersionableDbEntityConfiguration<CodingTaskEntity, string>
{
    protected override void OverrideConfigure(EntityTypeBuilder<CodingTaskEntity> modelBuilder)
    {
        base.OverrideConfigure(modelBuilder);
        modelBuilder.ToTable("CodingTasks");
    }
}