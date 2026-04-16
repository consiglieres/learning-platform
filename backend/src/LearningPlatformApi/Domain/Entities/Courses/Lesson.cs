using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.Entities.Tasks;
using LearningPlatformApi.Domain.Exceptions;

namespace LearningPlatformApi.Domain.Entities.Courses;

public record Lesson : VersionableEntity<string>
{
    public string Name { get; private set; }

    public int LessonOrder { get; private set; }

    public int PassThreshold { get; private set; }

    public Page.Page PageContent { get; private set; }

    public string ModuleId { get; private set; }

    public Module Module { get; private set; } = null!;
    
    public List<BaseTask> Tasks { get; set; }
    
    public Lesson(string name, int lessonOrder, int passThreshold, Page.Page page, Module module, User creator)
        : base(Guid.NewGuid().ToString())
    {
        Name = name;
        LessonOrder = lessonOrder;
        PassThreshold = passThreshold;
        PageContent = page;
        Module = module;
        Tasks = [];
        ModuleId = module.Id;
        MarkAsCreated(creator, DateTimeOffset.UtcNow);
    }

    public void AddTask(BaseTask task)
    {
        if (Tasks.Any(t => t.Order == task.Order))
            throw new DomainException($"Task with order {task.Order} already exists");

        Tasks.Add(task);
    }
}