using LearningPlatformApi.Domain.ValueObjects.Task;

namespace LearningPlatformApi.V1.Models.Tasks;

public class V1TaskShortInfo
{
    public required string Name { get; set; }

    public int Order { get; set; }

    public required Difficulty Difficulty { get; set; }
}