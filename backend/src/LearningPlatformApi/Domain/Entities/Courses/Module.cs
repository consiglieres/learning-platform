using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Domain.ValueObjects.Page;

namespace LearningPlatformApi.Domain.Entities.Courses;

public record Module : AuditableEntity<string>
{
    public Module(string name, int moduleOrder, string courseId, User creator,
        IReadOnlyCollection<Lesson> lessons)
        : base(Guid.NewGuid().ToString())
    {
        Name = name;
        ModuleOrder = moduleOrder;
        CourseId = courseId;
        Lessons = lessons.ToList();
        Page = Entities.Page.Page.EmptyPage(PageType.Introduction, creator);
        MarkAsCreated(creator, DateTimeOffset.UtcNow);
    }
    
    public Module(string id, string name, int moduleOrder, string courseId, User creator,
        IReadOnlyCollection<Lesson> lessons)
        : base(id)
    {
        Name = name;
        ModuleOrder = moduleOrder;
        CourseId = courseId;
        Lessons = lessons.ToList();
        Page = Entities.Page.Page.EmptyPage(PageType.Introduction, creator);
        MarkAsCreated(creator, DateTimeOffset.UtcNow);
    }

    public string Name { get; set; }
    public int ModuleOrder { get; set; }
    public string CourseId { get; set; }

    public List<Lesson> Lessons { get; set; }

    public Page.Page Page { get; set; }

    public void AddLesson(Lesson lesson)
    {
        if (Lessons.Any(t => t.LessonOrder == lesson.LessonOrder))
            throw new DomainException($"Lesson with order {lesson.LessonOrder} already exists");

        Lessons.Add(lesson);
    }
}