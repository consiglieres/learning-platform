namespace LearningPlatformApi.Persistence.Entities.Base;

public abstract class VersionableDbEntity<TKey>(TKey id) : AuditableDbEntity<TKey>(id)
{
    public int Order { get; set; }

    public string? Tag { get; set; }
}