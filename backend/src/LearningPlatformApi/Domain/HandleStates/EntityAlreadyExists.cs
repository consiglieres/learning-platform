namespace LearningPlatformApi.Domain.HandleStates;

public record struct EntityAlreadyExists(string Identification, string EntityName);