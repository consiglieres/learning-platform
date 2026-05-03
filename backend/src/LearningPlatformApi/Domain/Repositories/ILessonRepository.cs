using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Persistence.Repositories.Base;

namespace LearningPlatformApi.Domain.Repositories;

public interface ILessonRepository : IVersionedRepository<Lesson, string>
{
    public Task<IReadOnlyCollection<Lesson>> GetLastAsync(IReadOnlyCollection<string> ids,
        CancellationToken cancellationToken = default);
}