using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Persistence.Repositories.Base;

namespace LearningPlatformApi.Domain.Repositories;

public interface IModulesRepository : IVersionedRepository<Module, string>
{
    public Task<IReadOnlyCollection<Module>> GetLastAsync(IReadOnlyCollection<string> ids, CancellationToken cancellationToken = default);
}