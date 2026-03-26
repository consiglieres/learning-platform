using LearningPlatformApi.Persistence.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformApi.Persistence.Context;

public class ApplicationContext : IdentityDbContext<UserEntity>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<Resource> Resources { get; set; }
    public DbSet<UserResource> UserResources { get; set; }

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
    }
}