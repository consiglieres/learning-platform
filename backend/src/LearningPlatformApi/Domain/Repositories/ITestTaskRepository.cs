using LearningPlatformApi.Domain.Entities.Tasks;
using LearningPlatformApi.Persistence.Repositories.Base;

namespace LearningPlatformApi.Domain.Repositories;

public interface ITestTaskRepository : IAuditableRepository<TestTask, string>
{
    Task<IReadOnlyCollection<TestTask>> GetByIdsAsync(IReadOnlyCollection<string> ids, 
        CancellationToken cancellationToken = default);
}