using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Domain.ValueObjects.Page;

namespace LearningPlatformApi.Domain.Entities.Courses;

public record Module : VersionableEntity<string>
{
    public string Name { get; private set; }
    public int ModuleOrder { get; private set; }
    public string CourseId { get; private set; }

    private readonly List<Lesson> lessons = new();

    public IReadOnlyCollection<Lesson> Lessons => lessons.AsReadOnly();

    public Page.Page IntroductionPage { get; }

    private Module(string id) : base(id) { }

    public Module(string name, int moduleOrder, string courseId, User creator)
        : base(Guid.NewGuid().ToString())
    {
        Name = name;
        ModuleOrder = moduleOrder;
        CourseId = courseId;
        IntroductionPage = Page.Page.EmptyPage(PageType.Introduction);
        MarkAsCreated(creator, DateTimeOffset.UtcNow);
    }

    public void AddLesson(Lesson lesson)
    {
        if (lessons.Any(t => t.LessonOrder == lesson.LessonOrder))
            throw new DomainException($"Lesson with order {lesson.LessonOrder} already exists");

        lessons.Add(lesson);
    }
}