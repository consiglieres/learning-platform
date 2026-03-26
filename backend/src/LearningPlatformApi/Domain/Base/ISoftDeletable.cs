using LearningPlatformApi.Domain.Entities;

namespace LearningPlatformApi.Domain.Base;

public interface ISoftDeletable
{
    void MarkAsDeleted(User deletedBy, DateTimeOffset deletedAt);
    void Restore();
}