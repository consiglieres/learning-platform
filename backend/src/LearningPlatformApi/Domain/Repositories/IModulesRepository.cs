using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Persistence.Repositories.Base;

namespace LearningPlatformApi.Domain.Repositories;

public interface IModulesRepository : IAuditableRepository<Module, string>
{
}