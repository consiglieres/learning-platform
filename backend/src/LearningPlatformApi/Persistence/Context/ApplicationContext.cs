using LearningPlatformApi.Domain.ValueObjects.Page;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Entities.Base;
using LearningPlatformApi.Persistence.Entities.Page;
using LearningPlatformApi.Persistence.Entities.ValueConverters;
using LearningPlatformApi.Persistence.EntitiesConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformApi.Persistence.Context;

public class ApplicationContext : IdentityDbContext<UserEntity>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<Resource> Resources => Set<Resource>();
    public DbSet<UserResource> UserResources => Set<UserResource>();

    public DbSet<ContentBlockEntity> ContentBlocks => Set<ContentBlockEntity>();

    public DbSet<PageEntity> Pages => Set<PageEntity>();

    public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();

    public DbSet<CodingTaskEntity> CodingTasks => Set<CodingTaskEntity>();

    public DbSet<TestTaskEntity> TestTask => Set<TestTaskEntity>();

    public DbSet<CourseEntity> Courses => Set<CourseEntity>();

    public DbSet<LessonEntity> Lessons => Set<LessonEntity>();

    public DbSet<ModuleEntity> Modules => Set<ModuleEntity>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserResource>()
            .HasKey(ur => new { ur.UserId, ur.ResourceId });

        builder.Entity<UserResource>()
            .HasOne(ur => ur.UserEntity)
            .WithMany(u => u.UserResources)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<UserResource>()
            .HasOne(ur => ur.Resource)
            .WithMany(r => r.UserResources)
            .HasForeignKey(ur => ur.ResourceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ApplyConfiguration(new CategoriesEntityConfiguration());
        builder.ApplyConfiguration(new CodingTaskEntityConfiguration());
        builder.ApplyConfiguration(new ContentBlockEntityConfiguration());
        builder.ApplyConfiguration(new CourseEntityConfiguration());
        builder.ApplyConfiguration(new LessonEntityConfiguration());
        builder.ApplyConfiguration(new ModuleEntityConfiguration());
        builder.ApplyConfiguration(new PageEntityConfiguration());
        builder.ApplyConfiguration(new TestTaskEntityConfiguration());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<Enum>()
            .HaveConversion<string>();

        configurationBuilder.Properties<ContentBlockType>()
            .HaveConversion<ContentBlockTypeConverter>();
    }
}