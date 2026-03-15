namespace LearningPlatformApi.Domain.HandleStates;

public record struct OperationNotSucceeded<TInfo>(TInfo OperationInfo);
