using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.ValueObjects.Task;

namespace LearningPlatformApi.Domain.Entities.Tasks;

public abstract record BaseTask : AuditableEntity<string>
{
    public string Name { get; private set; }
    public int Order { get; private set; }
    public Difficulty Difficulty { get; private set; }

    public string LessonId { get; private set; }

    public Lesson Lesson { get; private set; } = null!;

    public CoursePage PageContent { get; private set; }

    protected BaseTask(string id, string name, int order, Difficulty difficulty, Lesson lesson, CoursePage pageContent)
        : base(id)
    {
        Name = name;
        Order = order;
        Difficulty = difficulty;
        Lesson = lesson;
        LessonId = lesson.Id;
        PageContent = pageContent;
    }

    public abstract bool CheckAnswer(object answer);
}