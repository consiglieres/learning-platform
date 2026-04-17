using LearningPlatformApi.Domain.ValueObjects.Task;

namespace LearningPlatformApi.Domain.Entities.Tasks;

public record CodingTask : BaseTask
{
    public string InitialCode { get; private set; }
    public string TestCode { get; private set; }
    public CodingTask(string name, int order, Difficulty difficulty, string lessonId, Page.Page page,
        string initialCode, string testCode, User createdBy)
        : base(Guid.NewGuid().ToString(), name, order, difficulty, lessonId, page)
    {
        InitialCode = initialCode;
        TestCode = testCode;
        MarkAsCreated(createdBy, DateTimeOffset.UtcNow);
    }

    public override bool CheckAnswer(object answer)
    {
        throw new NotImplementedException();
    }
}