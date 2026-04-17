using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Domain.Repositories;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Persistence.Context;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Repositories.Base.Impl;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformApi.Persistence.Repositories;

public class CoursesRepository(
    ApplicationContext context,
    IDbEntityMapper<Course, string, CourseEntity, string> mapper,
    ILogger<CoursesRepository> logger)
    : PublicationWorkflowRepository<Course, string, CourseEntity, string>(context, mapper, logger), ICourseRepository
{
    public new async Task<Course> GetAsync(string id, EntityVersion version,
        CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<CourseEntity>()
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .Include(x => x.Categories)
            .Include(x => x.IntroductionPage)
            .ThenInclude(x => x.ContentBlocks)
            .Include(x => x.Modules)
            .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.VersionOrder == version.Order, cancellationToken);

        if (entities == null) throw new DomainException("Entity not found");

        return mapper.Map(entities);
    }

    public new async Task<Course> GetLastAsync(string id, CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<CourseEntity>()
            .Where(x => x.Id.Equals(id))
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .Include(x => x.Categories)
            .Include(x => x.IntroductionPage)
            .ThenInclude(x => x.ContentBlocks)
            .Include(x => x.Modules)
            .OrderByDescending(x => x.VersionOrder)
            .LastOrDefaultAsync(cancellationToken);

        if (entities == null) throw new DomainException("Entity not found");

        return mapper.Map(entities);
    }
}