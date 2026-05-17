using LearningPlatformApi.Domain.Entities.Tasks;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Domain.Repositories;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Persistence.Context;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Entities.Page;
using LearningPlatformApi.Persistence.Repositories.Base.Impl;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformApi.Persistence.Repositories;

public class CodingTaskRepository(ApplicationContext context,
    IDbEntityMapper<CodingTask, string, CodingTaskEntity, string> mapper, ILogger<CodingTaskRepository> logger)
    : AuditableRepository<CodingTask, string, CodingTaskEntity, string>(context, mapper, logger),
        ICodingTaskRepository
{
    public override async Task<CodingTask?> FindByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Set<CodingTaskEntity>()
            .Where(x => x.Id.Equals(id))
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .Include(x => x.Lesson)
            .Include(x => x.Page)
            .ThenInclude(x => x.ContentBlocks)
            .FirstOrDefaultAsync(cancellationToken);
        
        return entity == null ? null : mapper.Map(entity);
    }
    
    public override async Task<CodingTask> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<CodingTaskEntity>()
            .Where(x => x.Id.Equals(id))
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .Include(x => x.Lesson)
            .Include(x => x.Page)
            .ThenInclude(x => x.ContentBlocks)
            .FirstOrDefaultAsync(cancellationToken);

        if (entities == null) throw new DomainException("Entity not found");

        return mapper.Map(entities);
    }
    
    public async Task<IReadOnlyCollection<CodingTask>> GetByIdsAsync(IReadOnlyCollection<string> ids, 
        CancellationToken cancellationToken = default)
    {
        var tasks = ids.Select(async x => await GetByIdAsync(x, cancellationToken));
        return await Task.WhenAll(tasks);
    }
    
    public override async Task<CodingTask> UpdateAsync(CodingTask entity, CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<CodingTaskEntity>()
            .Where(x => x.Id.Equals(entity.Id))
            .FirstOrDefaultAsync(cancellationToken);
        
        if(entities == null) throw new DomainException("Entity not found");
        
        entities.Name = entity.Name;
        entities.Order = entity.Order;
        entities.DifficultyCategory = entity.Difficulty.Name;
        entities.DifficultyPoints = entity.Difficulty.BasePoints;
        entities.InitialCode = entity.InitialCode;
        entities.TestCode = entity.TestCode;
        
        await context.SaveChangesAsync(cancellationToken);
        
        return await GetByIdAsync(entity.Id, cancellationToken);
    }
    
    public override async Task CreateAsync(CodingTask entity, CancellationToken cancellationToken = default)
    {
        var codingTaskEntity = mapper.Map(entity);
        var page = await context.Set<PageEntity>()
            .FirstOrDefaultAsync(x => x.Id == codingTaskEntity.PageId, cancellationToken);

        if (page != null)
        {
            codingTaskEntity.Page = page;
        }

        await context.Set<CodingTaskEntity>().AddAsync(codingTaskEntity, cancellationToken);
    }
}