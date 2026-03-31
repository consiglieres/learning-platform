using LearningPlatformApi.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration;

public sealed class CategoriesEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(k => new { k.TypeCode, k.ValueCode });

        builder.Property(x => x.TypeCode)
            .IsRequired();
        
        builder.Property(x => x.ValueCode)
            .IsRequired();
        
        builder.Property(x => x.TypeName)
            .IsRequired();
        
        builder.Property(x => x.ValueName)
            .IsRequired();
    }
}