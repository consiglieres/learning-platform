using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Persistence.Repositories.Base;

namespace LearningPlatformApi.Domain.Repositories;

public interface ILessonRepository : IAuditableRepository<Lesson, string>
{
    Task<IReadOnlyCollection<Lesson>> GetByIdsAsync(IReadOnlyCollection<string> ids, CancellationToken cancellationToken = default);
}