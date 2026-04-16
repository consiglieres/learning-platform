using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.ValueObjects.Task;

namespace LearningPlatformApi.Domain.Entities.Tasks;

public record TestTask : BaseTask
{
    public string Question { get; private set; }

    private readonly List<string> options = new();

    public IReadOnlyCollection<string> Options => options.AsReadOnly();

    public IReadOnlyCollection<string> Answer { get; set; }

    public TestTask(string name, int order, Difficulty difficulty, Lesson lesson, Page.Page? page,
        string question, IEnumerable<string> options, IEnumerable<string> correctAnswer)
        : base(Guid.NewGuid().ToString(), name, order, difficulty, lesson, page)
    {
        Question = question;
        this.options.AddRange(options);
        Answer = correctAnswer.ToList();
        MarkAsCreated(lesson.Module.CreatedBy, DateTimeOffset.UtcNow);
    }

    public override bool CheckAnswer(object answer)
    {
        return answer.Equals(Answer);
    }
}