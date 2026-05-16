namespace LearningPlatformApi.V1.Models.Account.Res;

public class V1UserResDto
{
    public required string Id { get; set; }

    public required string Email { get; set; }
    
    public required string FullName { get; set; }

    public DateTimeOffset? LastLoginAt { get; set; }

    public bool IsActive { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}