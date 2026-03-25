using LearningPlatformApi.Domain.ValueObjects;

namespace LearningPlatformApi.Domain.Base;

public interface IVersionable
{
    EntityVersion CurrentVersion { get; }

    EntityVersion LatestVersion { get; }

    IReadOnlyCollection<EntityVersion> Versions { get; }
}