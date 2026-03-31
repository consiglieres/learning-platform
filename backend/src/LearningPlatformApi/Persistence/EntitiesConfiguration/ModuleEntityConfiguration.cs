using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.EntitiesConfiguration.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration;

public class ModuleEntityConfiguration : VersionableDbEntityConfiguration<ModuleEntity, string>
{
    protected override void OverrideConfigure(EntityTypeBuilder<ModuleEntity> modelBuilder)
    {
        modelBuilder.HasKey(x => x.Id);
    }
}