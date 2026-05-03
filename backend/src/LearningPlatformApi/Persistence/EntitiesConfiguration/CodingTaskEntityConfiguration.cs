using LearningPlatformApi.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration;

public class CodingTaskEntityConfiguration : IEntityTypeConfiguration<CodingTaskEntity>
{
    public void Configure(EntityTypeBuilder<CodingTaskEntity> builder)
    {
        builder.ToTable("CodingTasks");
    }
}