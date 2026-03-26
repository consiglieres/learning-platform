using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.ValueObjects.Task;

namespace LearningPlatformApi.Domain.Entities.Tasks;

public record CodingTask : BaseTask
{
    public string InitialCode { get; private set; }
    public string TestCode { get; private set; }

    public CodingTask(string name, int order, Difficulty difficulty, Lesson lesson, CoursePage page,
        string initialCode, string testCode)
        : base(Guid.NewGuid().ToString(), name, order, difficulty, lesson, page)
    {
        InitialCode = initialCode;
        TestCode = testCode;
        MarkAsCreated(lesson.Module.CreatedBy, DateTimeOffset.UtcNow);
    }

    public override bool CheckAnswer(object answer)
    {
        var code = answer as string;
        return !string.IsNullOrEmpty(code);
    }
}