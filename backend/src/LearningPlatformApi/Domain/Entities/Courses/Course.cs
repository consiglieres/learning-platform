using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.Domain.ValueObjects.Page;

namespace LearningPlatformApi.Domain.Entities.Courses;

public record Course : PublicationWorkflowEntity<string>
{
    public string Title { get; private set; }
    public string Description { get; private set; }

    private readonly List<TypedCategory> categories = new();

    public IReadOnlyCollection<TypedCategory> Categories => categories.AsReadOnly();

    private readonly List<Module> modules = new();

    public IReadOnlyCollection<Module> Modules => modules.AsReadOnly();

    public CoursePage IntroductionCoursePage { get; }

    public Course(string title, string description, User creator) : base(Guid.NewGuid().ToString())
    {
        Title = title;
        Description = description;
        IntroductionCoursePage = CoursePage.EmptyPage(PageType.Introduction);
        MarkAsCreated(creator, DateTimeOffset.UtcNow);
    }

    public void AddModule(Module module)
    {
        if (modules.Any(m => m.ModuleOrder == module.ModuleOrder))
            throw new DomainException($"Module with order {module.ModuleOrder} already exists");

        modules.Add(module);
    }

    public void AddCategory(TypedCategory category)
    {
        if (categories.Any(c => c.Type == category.Type && c.Value == category.Value))
            throw new DomainException("Category already added");

        categories.Add(category);
    }

    public override bool CanBeSubmitted()
    {
        throw new NotImplementedException();
    }
}