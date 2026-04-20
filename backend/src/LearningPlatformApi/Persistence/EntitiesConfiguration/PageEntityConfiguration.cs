using LearningPlatformApi.Persistence.Entities.Page;
using LearningPlatformApi.Persistence.EntitiesConfiguration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration;

public class PageEntityConfiguration : VersionableDbEntityConfiguration<PageEntity, string>
{
    protected override void OverrideConfigure(EntityTypeBuilder<PageEntity> modelBuilder)
    {
        base.OverrideConfigure(modelBuilder);
        modelBuilder.ToTable("Pages");
            
        modelBuilder.HasMany(e => e.ContentBlocks)
            .WithOne()
            .HasForeignKey(e => new { e.PageId, e.PageVersion })
            .HasPrincipalKey(e => new { e.Id, e.VersionOrder });
        
        modelBuilder.Property(x => x.Order)
            .IsRequired();
        modelBuilder.Property(x => x.TypeCode)
            .IsRequired();
        modelBuilder.Property(x => x.TypeName)
            .IsRequired();
    }
}