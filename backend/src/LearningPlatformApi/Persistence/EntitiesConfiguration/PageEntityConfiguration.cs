using LearningPlatformApi.Persistence.Entities.Page;
using LearningPlatformApi.Persistence.EntitiesConfiguration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration;

public class PageEntityConfiguration : AuditableDbEntityConfiguration<PageEntity, string>
{
    protected override void OverrideConfigure(EntityTypeBuilder<PageEntity> modelBuilder)
    {
        base.OverrideConfigure(modelBuilder);
        modelBuilder.ToTable("Pages");

        modelBuilder.HasMany(e => e.ContentBlocks)
            .WithOne()
            .HasForeignKey(e => new { e.PageId })
            .HasPrincipalKey(e => new { e.Id });

        modelBuilder.Property(x => x.Order)
            .IsRequired();
        modelBuilder.Property(x => x.TypeCode)
            .IsRequired();
        modelBuilder.Property(x => x.TypeName)
            .IsRequired();
    }
}