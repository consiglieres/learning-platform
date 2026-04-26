using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Domain.ValueObjects.Page;

namespace LearningPlatformApi.Domain.Entities.Courses;

public record Module : VersionableEntity<string>
{
    public Module(string name, int moduleOrder, string courseId, User creator,
        IReadOnlyCollection<Lesson> lessons)
        : base(Guid.NewGuid().ToString())
    {
        Name = name;
        ModuleOrder = moduleOrder;
        CourseId = courseId;
        Lessons = lessons.ToList();
        IntroductionPage = Page.Page.EmptyPage(PageType.Introduction, creator);
        MarkAsCreated(creator, DateTimeOffset.UtcNow);
    }

    public string Name { get; private set; }
    public int ModuleOrder { get; private set; }
    public string CourseId { get; private set; }

    public List<Lesson> Lessons { get; set; }

    public Page.Page IntroductionPage { get; set; }

    public void AddLesson(Lesson lesson)
    {
        if (Lessons.Any(t => t.LessonOrder == lesson.LessonOrder))
            throw new DomainException($"Lesson with order {lesson.LessonOrder} already exists");

        Lessons.Add(lesson);
    }
}