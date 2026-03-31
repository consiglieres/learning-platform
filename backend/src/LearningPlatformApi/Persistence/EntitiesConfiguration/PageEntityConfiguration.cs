using LearningPlatformApi.Persistence.Entities.Page;
using LearningPlatformApi.Persistence.EntitiesConfiguration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration;

public class PageEntityConfiguration : VersionableDbEntityConfiguration<PageEntity, string>
{
    protected override void OverrideConfigure(EntityTypeBuilder<PageEntity> modelBuilder)
    {
        modelBuilder.ToTable("Pages");
        
        modelBuilder.HasKey(x => x.Id);
        modelBuilder.Property(x => x.Order)
            .IsRequired();
        modelBuilder.Property(x => x.TypeCode)
            .IsRequired();
        modelBuilder.Property(x => x.TypeName)
            .IsRequired();
        
    }
}