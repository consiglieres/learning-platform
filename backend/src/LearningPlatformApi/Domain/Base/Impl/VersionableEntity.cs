using LearningPlatformApi.Domain.ValueObjects;

namespace LearningPlatformApi.Domain.Base.Impl;

public abstract record VersionableEntity<TKey> : AuditableEntity<TKey>, IVersionable
{
    protected VersionableEntity(TKey Id) : base(Id)
    {
        Version = EntityVersion.CreateDefault();
    }

    protected VersionableEntity(TKey Id, EntityVersion Version) : base(Id)
    {
        this.Version = Version;
    }

    public EntityVersion Version { get; set; }

    public void Deconstruct(out TKey id, out EntityVersion version)
    {
        id = Id;
        version = Version;
    }
}