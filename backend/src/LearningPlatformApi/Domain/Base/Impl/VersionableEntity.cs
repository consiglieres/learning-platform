using LearningPlatformApi.Domain.ValueObjects;

namespace LearningPlatformApi.Domain.Base.Impl;

public abstract record VersionableEntity<TKey> : AuditableEntity<TKey>, IVersionable
{
    protected VersionableEntity(TKey Id) : base(Id)
    {
        Version = EntityVersion.CreateDefault();
        AllVersions = [Version];
    }

    protected VersionableEntity(TKey Id, EntityVersion Version, IReadOnlyCollection<EntityVersion> AllVersions) : base(Id)
    {
        this.Version = Version;
        this.AllVersions = AllVersions;
    }

    public EntityVersion CurrentVersion => Version;

    public EntityVersion LatestVersion => Versions.Last();
    public IReadOnlyCollection<EntityVersion> Versions => AllVersions;
    public EntityVersion Version { get; init; }
    public IReadOnlyCollection<EntityVersion> AllVersions { get; init; }

    public void Deconstruct(out TKey Id, out EntityVersion Version, out IReadOnlyCollection<EntityVersion> AllVersions)
    {
        Id = this.Id;
        Version = this.Version;
        AllVersions = this.AllVersions;
    }
}