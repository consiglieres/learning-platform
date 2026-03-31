using LearningPlatformApi.Persistence.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration.Base;

public abstract class PublicationDbEntityConfiguration<TEntity, TKey> : VersionableDbEntityConfiguration<TEntity, TKey>
    where TEntity : PublicationDbEntity<TKey>
{
    protected abstract override void OverrideConfigure(EntityTypeBuilder<TEntity> modelBuilder);
}