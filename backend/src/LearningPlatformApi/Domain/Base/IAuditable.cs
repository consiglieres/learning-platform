using LearningPlatformApi.Domain.Entities;

namespace LearningPlatformApi.Domain.Base;

public interface IAuditable
{
    DateTimeOffset CreatedAt { get; }
    User CreatedBy { get; }

    DateTimeOffset? UpdatedAt { get; }
    User? UpdatedBy { get; }

    DateTimeOffset? DeletedAt { get; }
    User? DeletedBy { get; }
    bool IsDeleted { get; }
}