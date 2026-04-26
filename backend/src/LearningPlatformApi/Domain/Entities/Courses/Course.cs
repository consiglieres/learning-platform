using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Domain.ValueObjects.Page;

namespace LearningPlatformApi.Domain.Entities.Courses;

public record Course : PublicationWorkflowEntity<string>
{
    public Course(string title, string description, User creator) : base(Guid.NewGuid().ToString())
    {
        Title = title;
        Description = description;
        IntroductionPage = Page.Page.EmptyPage(PageType.Introduction, creator);
        MarkAsCreated(creator, DateTimeOffset.UtcNow);
    }

    public string Title { get; set; }
    public string Description { get; set; }

    public List<TypedCategory> Categories { get; set; } = [];

    public List<Module> Modules { get; set; }

    public Page.Page IntroductionPage { get; set; }

    public void AddModule(Module module)
    {
        if (Modules.Any(m => m.ModuleOrder == module.ModuleOrder))
            throw new DomainException($"Module with order {module.ModuleOrder} already exists");

        Modules.Add(module);
    }

    public void AddCategory(TypedCategory category)
    {
        if (Categories.Any(c => c.Type == category.Type && c.Value == category.Value))
            throw new DomainException("Category already added");

        Categories.Add(category);
    }

    public void ResetCategories(IReadOnlyCollection<TypedCategory> categoriesList)
    {
        Categories.Clear();
        Categories.AddRange(categoriesList);
    }

    public void AddCategories(IReadOnlyCollection<TypedCategory> categoriesList)
    {
        if (Categories.Any(categoriesList.Contains)) throw new DomainException("Category already added");

        Categories.AddRange(categoriesList);
    }

    public override bool CanBeSubmitted()
    {
        throw new NotImplementedException();
    }
}