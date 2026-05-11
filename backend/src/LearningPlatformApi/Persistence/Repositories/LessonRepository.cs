using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Domain.Repositories;
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
    AuditableRepository<Lesson, string, LessonEntity, string>(context, lessonMapper, logger),
    ILessonRepository
{
    public override async Task<Lesson> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<LessonEntity>()
            .Where(x => x.Id.Equals(id))
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .Include(x => x.PageEntity)
            .ThenInclude(x => x.ContentBlocks)
            .Include(x => x.Module)
            .Include(x => x.Tasks)
            .FirstOrDefaultAsync(cancellationToken);

        if (entities == null) throw new DomainException("Entity not found");

        return lessonMapper.Map(entities);
    }
    
    public override async Task CreateAsync(Lesson entity, CancellationToken cancellationToken = default)
    {
        var lessonEntity = lessonMapper.Map(entity);
        var page = await context.Set<PageEntity>()
            .FirstOrDefaultAsync(x => x.Id == lessonEntity.PageEntity.Id, cancellationToken);

        if (page != null)
        {
            lessonEntity.PageEntity = page;
        }
        
        await context.Set<LessonEntity>().AddAsync(lessonEntity, cancellationToken);
    }
}