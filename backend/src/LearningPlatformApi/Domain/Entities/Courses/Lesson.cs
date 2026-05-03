using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.Entities.Tasks;
using LearningPlatformApi.Domain.Exceptions;

namespace LearningPlatformApi.Domain.Entities.Courses;

public record Lesson : VersionableEntity<string>
{
    public Lesson(string name, int lessonOrder, int passThreshold, Page.Page page, string moduleId, User creator)
        : base(Guid.NewGuid().ToString())
    {
        Name = name;
        LessonOrder = lessonOrder;
        PassThreshold = passThreshold;
        PageContent = page;
        Tasks = [];
        ModuleId = moduleId;
        MarkAsCreated(creator, DateTimeOffset.UtcNow);
    }

    public string Name { get; set; }

    public int LessonOrder { get; set; }

    public int PassThreshold { get; set; }

    public Page.Page PageContent { get; set; }

    public string ModuleId { get; set; }

    public List<BaseTask> Tasks { get; set; }

    public void AddTask(BaseTask task)
    {
        if (Tasks.Any(t => t.Order == task.Order))
            throw new DomainException($"Task with order {task.Order} already exists");

        Tasks.Add(task);
    }
}