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
    IDbEntityMapper<Course, string, CourseEntity, string> pageMapper,
    ILogger<CoursesRepository> logger)
    : PublicationWorkflowRepository<Course, string, CourseEntity, string>(context, pageMapper, logger),
        ICourseRepository
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

        return pageMapper.Map(entities);
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

        return pageMapper.Map(entities);
    }

    public override async Task CreateAsync(Course course, CancellationToken cancellationToken = default)
    {
        var courseEntity = pageMapper.Map(course);

        if (courseEntity.Categories.Any())
        {
            var allExisting = await context.Categories.ToListAsync(cancellationToken);

            var categoriesToAdd = new List<CategoryEntity>();

            foreach (var category in courseEntity.Categories)
            {
                var existing = allExisting
                    .FirstOrDefault(c => c.TypeName == category.TypeName && c.ValueName == category.ValueName);

                categoriesToAdd.Add(existing ?? category);
            }

            courseEntity.Categories = categoriesToAdd;
        }

        await context.Courses.AddAsync(courseEntity, cancellationToken);
    }

    public override async Task<Course> UpdateAsync(Course entity, CancellationToken cancellationToken = default)
    {
        var dbId = entity.Id;
        var dbEntity = await context.Set<CourseEntity>()
            .Include(x => x.Categories)
            .FirstOrDefaultAsync(x => x.Id.Equals(dbId) && x.VersionOrder == entity.Version.Order,
                cancellationToken);

        if (dbEntity == null) throw new DomainException("Entity not found");

        var updated = pageMapper.Map(entity);
        context.Entry(dbEntity).CurrentValues.SetValues(updated);

        await UpdateCategoriesAsync(dbEntity, entity.Categories, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return await GetAsync(entity.Id, entity.Version, cancellationToken);
    }

    private async Task UpdateCategoriesAsync(CourseEntity dbEntity, IReadOnlyCollection<TypedCategory> newCategories,
        CancellationToken cancellationToken)
    {
        if (!newCategories.Any())
        {
            dbEntity.Categories.Clear();
            return;
        }

        var allExistingCategories = await context.Categories.ToListAsync(cancellationToken);

        var newCategoryEntities = newCategories.Select(c => new CategoryEntity
        {
            TypeName = c.Type,
            ValueName = c.Value
        }).ToList();

        var toAdd = new List<CategoryEntity>();
        foreach (var newCat in newCategoryEntities)
        {
            var existing = allExistingCategories
                .FirstOrDefault(c => c.TypeName == newCat.TypeName && c.ValueName == newCat.ValueName);

            if (existing != null)
                toAdd.Add(existing);
            else
                toAdd.Add(newCat);
        }

        dbEntity.Categories.Clear();
        foreach (var category in toAdd) dbEntity.Categories.Add(category);
    }
}