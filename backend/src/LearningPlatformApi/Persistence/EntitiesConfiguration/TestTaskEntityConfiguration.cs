using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Entities.Base;
using LearningPlatformApi.Persistence.EntitiesConfiguration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatformApi.Persistence.EntitiesConfiguration;

public class TestTaskEntityConfiguration : AuditableDbEntityConfiguration<TestTaskEntity, string>
{
    protected override void OverrideConfigure(EntityTypeBuilder<TestTaskEntity> entityTypeBuilder)
    {
        base.OverrideConfigure(entityTypeBuilder);
        entityTypeBuilder.HasOne(x => x.Lesson)
            .WithMany()
            .HasForeignKey(x => x.LessonId)
            .HasPrincipalKey(x => x.Id);
        
        entityTypeBuilder.HasOne(x => x.Page)
            .WithMany()
            .HasForeignKey(x => x.PageId)
            .HasPrincipalKey(x => x.Id);
        entityTypeBuilder.ToTable("TestTask");
    }
}