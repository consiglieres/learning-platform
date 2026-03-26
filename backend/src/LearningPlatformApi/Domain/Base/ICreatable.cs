using LearningPlatformApi.Domain.Entities;

namespace LearningPlatformApi.Domain.Base;

public interface ICreatable
{
    void MarkAsCreated(User cratedBy, DateTimeOffset createdAt);
}