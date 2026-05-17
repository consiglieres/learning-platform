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

public class LessonRepository(ApplicationContext context, ICodingTaskRepository codingTaskRepository,
    ITestTaskRepository testTaskRepository, IDbEntityMapper<Lesson, string, LessonEntity, string> lessonMapper, 
    ILogger<LessonRepository> logger)
    : AuditableRepository<Lesson, string, LessonEntity, string>(context, lessonMapper, logger),
    ILessonRepository
{
    public override async Task<Lesson> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Set<LessonEntity>()
            .Where(x => x.Id.Equals(id))
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .Include(x => x.PageEntity)
            .ThenInclude(x => x.ContentBlocks)
            .Include(x => x.Module)
            .Include(x => x.CodingTasks)
            .Include(x => x.TestTasks)
            .FirstOrDefaultAsync(cancellationToken);
        if (entity == null) throw new DomainException("Entity not found");
        
        var lesson = lessonMapper.Map(entity);
        
        var codingTasks = await codingTaskRepository.GetByIdsAsync(
            lesson.CodingTasks.Select(x => x.Id).ToList(), cancellationToken);
        lesson.CodingTasks = codingTasks.ToList();
        
        var testTasks = await testTaskRepository.GetByIdsAsync(
            lesson.TestTasks.Select(x => x.Id).ToList(), cancellationToken);
        lesson.TestTasks = testTasks.ToList();

        return lessonMapper.Map(entity);
    }

    public async Task<IReadOnlyCollection<Lesson>> GetByIdsAsync(IReadOnlyCollection<string> ids, CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<LessonEntity>()
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .Include(x => x.PageEntity)
            .ThenInclude(x => x.ContentBlocks)
            .Include(x => x.Module)
            .Include(x => x.CodingTasks)
            .Include(x => x.TestTasks)
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);

        if (entities == null) throw new DomainException("Entity not found");

        return entities.Select(lessonMapper.Map).ToList();
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

    public override async Task<Lesson> UpdateAsync(Lesson entity, CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<LessonEntity>()
            .Where(x => x.Id.Equals(entity.Id))
            .FirstOrDefaultAsync(cancellationToken);
        
        if(entities == null) throw new DomainException("Entity not found");
        
        entities.Name = entity.Name;
        entities.LessonOrder = entity.LessonOrder;
        entities.PassThreshold = entity.PassThreshold;
        await context.SaveChangesAsync(cancellationToken);
        
        return await GetByIdAsync(entity.Id, cancellationToken);
    }
}