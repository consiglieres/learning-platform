namespace LearningPlatformApi.Domain.ValueObjects;

public record EntityVersion(int Order, string? Tag = null)
{
    public static EntityVersion CreateDefault()
    {
        return new EntityVersion(0);
    }

    public static EntityVersion SetStable(EntityVersion version)
    {
        return version with { Tag = version.Tag ?? DateTimeOffset.UtcNow.Ticks.ToString() };
    }

    public static EntityVersion IncrementVersion(EntityVersion version)
    {
        return version with { Order = version.Order + 1 };
    }
}