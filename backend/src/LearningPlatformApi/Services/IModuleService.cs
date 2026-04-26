using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.V1.Models.Module.Req;
using LearningPlatformApi.V1.Models.Module.Res;


namespace LearningPlatformApi.Services;

public interface IModuleService
{
    Task<V1ModuleResDto> CreateAsync(CreateModuleRequest request, User user, CancellationToken cancellationToken);

    Task<V1ModuleResDto> UpdateAsync(string id, User user, UpdateModuleRequest request, CancellationToken cancellationToken);

    Task<V1ModuleResDto> GetLatestAsync(string id, CancellationToken cancellationToken);

    Task<V1ModuleResDto> GetByVersionAsync(string id, int versionOrder, CancellationToken cancellationToken);

    Task DeleteAsync(string id, User user, CancellationToken cancellationToken);

    Task<V1ModuleResDto> RestoreAsync(string id, User user, CancellationToken cancellationToken);

    Task<V1ModuleResDto> RollbackToVersionAsync(string id, int targetVersionOrder, string? reason, CancellationToken cancellationToken);

    Task<List<ModuleVersionInfoDto>> GetVersionHistoryAsync(string id, int limit, CancellationToken cancellationToken);

    Task<ModuleComparisonResDto> CompareVersionsAsync(string id, int sourceVersion, int targetVersion, CancellationToken cancellationToken);

    Task<V1ModuleResDto> CopyModuleAsync(CopyModuleRequest request, User user, CancellationToken cancellationToken);

    Task<List<V1ModuleResDto>> GetModulesByIdsAsync(List<string> ids, CancellationToken cancellationToken);

    Task<V1ModuleResDto> ReorderLessonsAsync(string id, List<string> lessonIds, CancellationToken cancellationToken);

    Task<V1ModuleResDto> UpdateModuleSettingsAsync(string id, V1ModuleSettingsDto settings, User user, CancellationToken cancellationToken);
}