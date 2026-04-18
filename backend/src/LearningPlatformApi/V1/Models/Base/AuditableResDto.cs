using LearningPlatformApi.V1.Models.Account.Res;

namespace LearningPlatformApi.V1.Models.Base;

public class AuditableResDto
{
    public required string Id { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public V1UserResDto CreatedBy { get; set; } = null!;
    
    public DateTimeOffset? UpdatedAt { get; set; }
    
    public V1UserResDto? UpdatedBy { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
    
    public V1UserResDto? DeletedBy { get; set; }
}