using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Domain.ValueObjects.Page;

namespace LearningPlatformApi.Domain.Entities.Courses;

public record Module : PublicationWorkflowEntity<string>
{
    public string Name { get; private set; }
    public int Order { get; private set; }
    public string CourseId { get; private set; }

    private readonly List<Lesson> lessons = new();

    public IReadOnlyCollection<Lesson> Lessons => lessons.AsReadOnly();

    public CoursePage IntroductionPage { get; }

    private Module(string id) : base(id) { }

    public Module(string name, int order, string courseId, User creator)
        : base(Guid.NewGuid().ToString())
    {
        Name = name;
        Order = order;
        CourseId = courseId;
        IntroductionPage = CoursePage.EmptyPage(PageType.Introduction);
        MarkAsCreated(creator, DateTimeOffset.UtcNow);
    }

    public void AddLesson(Lesson lesson)
    {
        if (lessons.Any(t => t.Order == lesson.Order))
            throw new DomainException($"Lesson with order {lesson.Order} already exists");

        lessons.Add(lesson);
    }

    public override bool CanBeSubmitted()
    {
        throw new NotImplementedException();
    }
}