using LearningPlatformApi.Domain.Entities;

namespace LearningPlatformApi.Domain.Base;

public interface IUpdatable
{
    void MarkAsUpdated(User updatedBy, DateTimeOffset updatedAt);
}