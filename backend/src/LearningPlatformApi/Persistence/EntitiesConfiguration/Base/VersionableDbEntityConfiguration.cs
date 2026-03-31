using LearningPlatformApi.Persistence.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration.Base;

public abstract class VersionableDbEntityConfiguration<TEntity, TKey> : AuditableDbEntityConfiguration<TEntity, TKey>
    where TEntity : VersionableDbEntity<TKey>
{
    protected abstract override void OverrideConfigure(EntityTypeBuilder<TEntity> modelBuilder);
}