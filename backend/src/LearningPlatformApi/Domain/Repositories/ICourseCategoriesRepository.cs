using LearningPlatformApi.Domain.Entities.Courses;

namespace LearningPlatformApi.Domain.Repositories;

public interface ICourseCategoriesRepository
{
    public Task<IReadOnlyList<TypedCategory>> GetAllCategoriesAsync(CancellationToken cancellationToken = default);
}