using LearningPlatformApi.Domain.Entities;

namespace LearningPlatformApi.Domain.Base;

public interface ISoftDeletable
{
    void MarkAsDeleted(User deletedBy, DateTimeOffset deletedAt);

    void Restore(User deletedBy, DateTimeOffset deletedAt);

    bool IsDeleted();
}