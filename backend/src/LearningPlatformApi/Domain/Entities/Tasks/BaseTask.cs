using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.ValueObjects.Task;

namespace LearningPlatformApi.Domain.Entities.Tasks;

public abstract record BaseTask : VersionableEntity<string>
{
    public string Name { get; private set; }
    public int Order { get; private set; }
    public Difficulty Difficulty { get; private set; }

    public string LessonId { get; private set; }
    
    public Page.Page PageContent { get; private set; }
    
    protected BaseTask(string id, string name, int order, Difficulty difficulty, string lessonId, Page.Page pageContent)
        : base(id)
    {
        Name = name;
        Order = order;
        Difficulty = difficulty;
        PageContent = pageContent;
        LessonId = lessonId;
    }

    public abstract bool CheckAnswer(object answer);
}