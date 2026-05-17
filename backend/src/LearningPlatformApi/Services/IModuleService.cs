using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.V1.Models.Module.Req;
using LearningPlatformApi.V1.Models.Module.Res;


namespace LearningPlatformApi.Services;

public interface IModuleService
{
    Task<V1ModuleResDto> CreateAsync(CreateModuleRequest request, User user, CancellationToken cancellationToken);

    Task<V1ModuleResDto> UpdateAsync(string id, User user, UpdateModuleRequest request, CancellationToken cancellationToken);

    Task<V1ModuleResDto> GetByIdAsync(string id, CancellationToken cancellationToken);

    Task DeleteAsync(string id, User user, CancellationToken cancellationToken);

    Task<V1ModuleResDto> RestoreAsync(string id, User user, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<V1ModuleResDto>> GetModulesByIdsAsync(IReadOnlyCollection<string> ids,
        CancellationToken cancellationToken);

    Task<V1ModuleResDto> ReorderLessonsAsync(string id, List<string> lessonIds, CancellationToken cancellationToken);

    Task<V1ModuleResDto> UpdateModuleSettingsAsync(string id, V1ModuleSettingsDto settings, User user, CancellationToken cancellationToken);
}