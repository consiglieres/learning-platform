using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Repositories;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Persistence.Context;
using LearningPlatformApi.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformApi.Persistence.Repositories;

public class CourseCategoryRepository(
    ApplicationContext context,
    ICourseCategoryMapper courseCategoryMapper,
    ILogger<CoursesRepository> logger) : ICourseCategoriesRepository
{
    public async Task<IReadOnlyList<TypedCategory>> GetAllCategoriesAsync(CancellationToken cancellationToken = default)
    {
        var categoryEntities = await context.Set<CategoryEntity>()
            .ToListAsync(cancellationToken);

        return categoryEntities.Select(courseCategoryMapper.Map).ToList();
    }
}