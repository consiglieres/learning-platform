using LearningPlatformApi.Domain.ValueObjects.Task;

namespace LearningPlatformApi.Domain.Entities.Tasks;

public record TestTask : BaseTask
{
    public TestTask(string name, int order, Difficulty difficulty, string lessonId, Page.Page page,
        string question, IReadOnlyCollection<string> options, IEnumerable<string> correctAnswer, User createdBy)
        : base(Guid.NewGuid().ToString(), name, order, difficulty, lessonId, page)
    {
        Question = question;
        Options = options;
        Answer = correctAnswer.ToList();
        MarkAsCreated(createdBy, DateTimeOffset.UtcNow);
    }

    public string Question { get; set; }

    public IReadOnlyCollection<string> Options { get; set; }

    public IReadOnlyCollection<string> Answer { get; set; }

    public override bool CheckAnswer(object answer)
    {
        return answer.Equals(Answer);
    }
}