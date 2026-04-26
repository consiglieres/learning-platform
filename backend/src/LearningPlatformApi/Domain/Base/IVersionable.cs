using LearningPlatformApi.Domain.ValueObjects;

namespace LearningPlatformApi.Domain.Base;

public interface IVersionable
{
    EntityVersion Version { get; set; }
}