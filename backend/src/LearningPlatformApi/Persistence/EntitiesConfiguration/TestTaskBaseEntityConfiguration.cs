using LearningPlatformApi.Persistence.Entities.Base;
using LearningPlatformApi.Persistence.EntitiesConfiguration.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration;

public class TestTaskBaseEntityConfiguration : AuditableDbEntityConfiguration<TaskBaseEntity, string>
{
    protected override void OverrideConfigure(EntityTypeBuilder<TaskBaseEntity> entityTypeBuilder)
    {
        base.OverrideConfigure(entityTypeBuilder);
    }
}