namespace LearningPlatformApi.Persistence.Entities;

public class Resource
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public Guid OwnerId { get; set; }

    public virtual ICollection<UserResource> UserResources { get; set; } = new List<UserResource>();
}