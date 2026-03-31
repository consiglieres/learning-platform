using LearningPlatformApi.Persistence.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration.Base;

public abstract class AuditableDbEntityConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
    where TEntity : AuditableDbEntity<TKey>
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        OverrideConfigure(builder);
    }

    protected abstract void OverrideConfigure(EntityTypeBuilder<TEntity> modelBuilder);
}