using LearningPlatformApi.Persistence.Entities.Base;

namespace LearningPlatformApi.Persistence.Entities;

public class TestTaskEntity(string id) : TaskBaseEntity(id)
{
    public string Question { get; set; }

    public IReadOnlyList<string> Options { get; set; }

    public IReadOnlyCollection<string> CorrectAnswer { get; set; }
}