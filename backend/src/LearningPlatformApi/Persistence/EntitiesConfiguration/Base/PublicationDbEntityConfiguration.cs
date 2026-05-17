using LearningPlatformApi.Persistence.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration.Base;

public abstract class PublicationDbEntityConfiguration<TEntity, TKey> : AuditableDbEntityConfiguration<TEntity, TKey>
    where TEntity : PublicationDbEntity<TKey>
{
    public new void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.OverrideConfigure(builder);
    }

    protected override void OverrideConfigure(EntityTypeBuilder<TEntity> modelBuilder)
    {
        Configure(modelBuilder);
    }
}