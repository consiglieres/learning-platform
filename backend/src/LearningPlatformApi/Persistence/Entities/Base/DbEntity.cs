namespace LearningPlatformApi.Persistence.Entities.Base;

public abstract class DbEntity<TId>(TId id)
{
    public TId Id { get; set; } = id;
}