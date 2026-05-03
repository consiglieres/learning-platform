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

public class LessonRepository(
    ApplicationContext context,
    IDbEntityMapper<Lesson, string, LessonEntity, string> lessonMapper,
    ILogger<LessonRepository> logger) :
    VersionedRepository<Lesson, string, LessonEntity, string>(context, lessonMapper, logger),
    ILessonRepository
{
    public override async Task<Lesson> GetAsync(string id, EntityVersion version,
        CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<LessonEntity>()
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .Include(x => x.PageEntity)
            .ThenInclude(x => x.ContentBlocks)
            .Include(x => x.Module)
            .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.VersionOrder == version.Order, cancellationToken);

        if (entities == null) throw new DomainException("Entity not found");

        return lessonMapper.Map(entities);
    }
    
    public override async Task<Lesson> GetLastAsync(string id, CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<LessonEntity>()
            .Where(x => x.Id.Equals(id))
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .Include(x => x.PageEntity)
            .ThenInclude(x => x.ContentBlocks)
            .Include(x => x.Module)
            .OrderByDescending(x => x.VersionOrder)
            .FirstOrDefaultAsync(cancellationToken);

        if (entities == null) throw new DomainException("Entity not found");

        return lessonMapper.Map(entities);
    }
    
    public async Task<IReadOnlyCollection<Lesson>> GetLastAsync(IReadOnlyCollection<string> ids, CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<LessonEntity>()
            .Where(x => ids.Contains(x.Id))
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .Include(x => x.PageEntity)
            .ThenInclude(x => x.ContentBlocks)
            .Include(x => x.Module)
            .OrderByDescending(x => x.VersionOrder)
            .Take(ids.Count)
            .ToListAsync(cancellationToken);

        if (entities == null) throw new DomainException("Entity not found");

        return entities.Select(lessonMapper.Map).ToList();
    }
    
    public override async Task CreateAsync(Lesson entity, CancellationToken cancellationToken = default)
    {
        var lessonEntity = lessonMapper.Map(entity);
        var page = await context.Set<PageEntity>()
            .FirstOrDefaultAsync(x => x.Id == lessonEntity.PageEntity.Id 
                                      && x.VersionOrder == lessonEntity.PageEntity.VersionOrder, cancellationToken);

        if (page != null)
        {
            lessonEntity.PageEntity = page;
        }
        
        await context.Set<LessonEntity>().AddAsync(lessonEntity, cancellationToken);
    }
}