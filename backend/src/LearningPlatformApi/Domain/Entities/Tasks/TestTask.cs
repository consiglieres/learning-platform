using LearningPlatformApi.Domain.ValueObjects.Task;

namespace LearningPlatformApi.Domain.Entities.Tasks;

public record TestTask : BaseTask
{
    private readonly List<string> options = new();

    public TestTask(string name, int order, Difficulty difficulty, string lessonId, Page.Page page,
        string question, IEnumerable<string> options, IEnumerable<string> correctAnswer, User createdBy)
        : base(Guid.NewGuid().ToString(), name, order, difficulty, lessonId, page)
    {
        Question = question;
        this.options.AddRange(options);
        Answer = correctAnswer.ToList();
        MarkAsCreated(createdBy, DateTimeOffset.UtcNow);
    }

    public string Question { get; private set; }

    public IReadOnlyCollection<string> Options => options.AsReadOnly();

    public IReadOnlyCollection<string> Answer { get; set; }

    public override bool CheckAnswer(object answer)
    {
        return answer.Equals(Answer);
    }
}