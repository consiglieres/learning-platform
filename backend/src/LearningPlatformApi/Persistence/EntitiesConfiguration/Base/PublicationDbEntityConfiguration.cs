using LearningPlatformApi.Persistence.Entities.Base;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration.Base;

public abstract class PublicationDbEntityConfiguration<TEntity, TKey> : VersionableDbEntityConfiguration<TEntity, TKey>
    where TEntity : PublicationDbEntity<TKey>
{
}