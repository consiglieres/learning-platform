using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.Entities.Tasks;
using LearningPlatformApi.Domain.Exceptions;

namespace LearningPlatformApi.Domain.Entities.Courses;

public record Lesson : PublicationWorkflowEntity<string>
{
    public string Name { get; private set; }

    public int Order { get; private set; }

    public int PassThreshold { get; private set; }

    public Page.CoursePage CoursePageContent { get; private set; }

    public string ModuleId { get; private set; }

    public Module Module { get; private set; } = null!;

    private readonly List<BaseTask> tasks = new();

    public IReadOnlyCollection<BaseTask> Tasks => tasks.AsReadOnly();

    private Lesson(string id) : base(id) { }

    public Lesson(string name, int order, int passThreshold, Module module, User creator)
        : base(Guid.NewGuid().ToString())
    {
        Name = name;
        Order = order;
        PassThreshold = passThreshold;
        Module = module;
        ModuleId = module.Id;
        MarkAsCreated(creator, DateTimeOffset.UtcNow);
    }

    public void AddTask(BaseTask task)
    {
        if (tasks.Any(t => t.Order == task.Order))
            throw new DomainException($"Task with order {task.Order} already exists");

        tasks.Add(task);
    }

    public override bool CanBeSubmitted()
    {
        throw new NotImplementedException();
    }
}