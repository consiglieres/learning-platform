using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.ValueObjects.Task;

namespace LearningPlatformApi.Domain.Entities.Tasks;

public abstract record BaseTask : AuditableEntity<string>
{
    protected BaseTask(string id, string name, int order, Difficulty difficulty, string lessonId, Page.Page pageContent)
        : base(id)
    {
        Name = name;
        Order = order;
        Difficulty = difficulty;
        PageContent = pageContent;
        LessonId = lessonId;
    }

    public string Name { get; set; }
    public int Order { get; set; }
    public Difficulty Difficulty { get; set; }

    public string LessonId { get; set; }

    public Page.Page PageContent { get; private set; }

    public abstract bool CheckAnswer(object answer);
}