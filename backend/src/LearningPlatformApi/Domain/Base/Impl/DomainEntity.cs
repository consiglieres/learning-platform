namespace LearningPlatformApi.Domain.Base.Impl;

public abstract record DomainEntity<TKey>(TKey Id) : IIdentifiable<TKey>;