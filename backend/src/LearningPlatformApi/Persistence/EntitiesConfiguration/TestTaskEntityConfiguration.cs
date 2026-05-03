using LearningPlatformApi.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration;

public class TestTaskEntityConfiguration : IEntityTypeConfiguration<TestTaskEntity>
{
    public void Configure(EntityTypeBuilder<TestTaskEntity> builder)
    {
        builder.ToTable("TestTask");
    }
}