using LearningPlatformApi.Domain.Entities.Tasks;
using LearningPlatformApi.Persistence.Repositories.Base;

namespace LearningPlatformApi.Domain.Repositories;

public interface ICodingTaskRepository : IAuditableRepository<CodingTask, string>
{
    Task<IReadOnlyCollection<CodingTask>> GetByIdsAsync(IReadOnlyCollection<string> ids, 
        CancellationToken cancellationToken = default);
}