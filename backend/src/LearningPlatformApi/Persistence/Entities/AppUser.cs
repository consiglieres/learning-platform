using Microsoft.AspNetCore.Identity;

namespace LearningPlatformApi.Persistence.Entities;

public class AppUser : IdentityUser
{
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? LastLoginAt { get; set; }
    
    // Navigation properties
    public virtual ICollection<UserResource> UserResources { get; set; } = new List<UserResource>();
}