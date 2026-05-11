using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Persistence.Repositories.Base;

namespace LearningPlatformApi.Domain.Repositories;

public interface IPageRepository : IAuditableRepository<Page, string>
{
}