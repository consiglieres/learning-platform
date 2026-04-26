using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Domain.Repositories;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Persistence.Context;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Entities.Page;
using LearningPlatformApi.Persistence.Repositories.Base.Impl;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformApi.Persistence.Repositories;

public class CoursesRepository(
    ApplicationContext context,
    IDbEntityMapper<Course, string, CourseEntity, string> courseMapper,
    ILogger<CoursesRepository> logger)
    : PublicationWorkflowRepository<Course, string, CourseEntity, string>(context, courseMapper, logger),
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

        return courseMapper.Map(entities);
    }

    public new async Task<Course> GetLastAsync(string id, CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<CourseEntity>()
            .Where(x => x.Id.Equals(id))
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .Include(x => x.Categories)
            .Include(x => x.Modules)
            .OrderByDescending(x => x.VersionOrder)
            .FirstOrDefaultAsync(cancellationToken);
    
        if (entities == null) throw new DomainException("Course not found");

        var page = await context.Set<PageEntity>()
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .Include(x => x.ContentBlocks)
            .OrderByDescending(x => x.VersionOrder)
            .FirstOrDefaultAsync(x => x.Id.Equals(entities.PageId), cancellationToken);
    
        if (page == null) throw new DomainException("Page not found");

        var moduleEntities = await context.Set<ModuleEntity>()
            .Where(m => m.CourseId == entities.Id && m.CourseVersion == entities.VersionOrder)
            .Where(m => m.VersionOrder == context.Set<ModuleEntity>()
                .Where(sub => sub.ModuleOrder == m.ModuleOrder)
                .Max(sub => sub.VersionOrder))
            .Include(m => m.Lessons)
            .Include(m => m.CreatedByUser)
            .Include(m => m.UpdatedByUser)
            .Include(m => m.DeletedByUser)
            .Include(m => m.IntroductionPage)
            .ThenInclude(p => p.CreatedByUser)
            .Include(m => m.IntroductionPage)
            .ThenInclude(p => p.UpdatedByUser)
            .Include(m => m.IntroductionPage)
            .ThenInclude(p => p.DeletedByUser)
            .Include(m => m.IntroductionPage)
            .ThenInclude(p => p.ContentBlocks)
            .ToListAsync(cancellationToken);

        entities.Modules = moduleEntities;
        entities.IntroductionPage = page;

        return courseMapper.Map(entities);
    }

    public override async Task CreateAsync(Course course, CancellationToken cancellationToken = default)
    {
        var courseEntity = courseMapper.Map(course);

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

        courseEntity.IntroductionPage = null!;

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

        entity.Version = EntityVersion.IncrementVersion(entity.Version);
        var updated = courseMapper.Map(entity);
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