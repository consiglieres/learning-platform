using LearningPlatformApi.Persistence.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration.Base;

public abstract class AuditableDbEntityConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
    where TEntity : AuditableDbEntity<TKey>
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        // Настройка связи с CreatedByUser
        builder.HasOne(e => e.CreatedByUser)
            .WithMany()
            .HasForeignKey(e => e.CreatedBy)
            .HasPrincipalKey(u => u.Id)
            .OnDelete(DeleteBehavior.Restrict);

        // Настройка связи с UpdatedByUser
        builder.HasOne(e => e.UpdatedByUser)
            .WithMany()
            .HasForeignKey(e => e.UpdatedBy)
            .HasPrincipalKey(u => u.Id)
            .OnDelete(DeleteBehavior.Restrict);

        // Настройка связи с DeletedByUser
        builder.HasOne(e => e.DeletedByUser)
            .WithMany()
            .HasForeignKey(e => e.DeletedBy)
            .HasPrincipalKey(u => u.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }

    protected virtual void OverrideConfigure(EntityTypeBuilder<TEntity> modelBuilder)
    {
        Configure(modelBuilder);
    }
}