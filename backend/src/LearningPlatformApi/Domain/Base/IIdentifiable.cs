namespace LearningPlatformApi.Domain.Base;

public interface IIdentifiable<TKey>
{
    public TKey Id { get; init; }
}