using Microsoft.AspNetCore.Identity;

namespace LearningPlatformApi.Persistence.Entities;

public class UserEntity : IdentityUser
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTimeOffset? LastLoginAt { get; set; }

    // Navigation properties
    public virtual ICollection<UserResource> UserResources { get; set; } = new List<UserResource>();
}