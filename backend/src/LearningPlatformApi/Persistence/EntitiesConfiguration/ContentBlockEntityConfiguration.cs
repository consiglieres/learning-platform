using LearningPlatformApi.Persistence.Entities.Page;
using LearningPlatformApi.Persistence.EntitiesConfiguration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration;

public sealed class ContentBlockEntityConfiguration : AuditableDbEntityConfiguration<ContentBlockEntity, string>
{
    protected override void OverrideConfigure(EntityTypeBuilder<ContentBlockEntity> modelBuilder)
    {
        modelBuilder.ToTable("ContentBlocks");
        modelBuilder.HasKey(x => x.Id);
        modelBuilder.HasIndex(x => new { x.PageId, x.Order })
            .IsUnique()
            .HasDatabaseName("IX_ContentBlocks_PageId");

        modelBuilder.Property(x => x.PageId)
            .IsRequired();
        modelBuilder.Property(x => x.Order)
            .IsRequired();
        modelBuilder.Property(x => x.Data)
            .IsRequired();
        modelBuilder.Property(x => x.Type)
            .IsRequired();
    }
}