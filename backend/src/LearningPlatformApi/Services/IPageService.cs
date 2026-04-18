using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.V1.Models.Page;
using LearningPlatformApi.V1.Models.Page.Req;
using LearningPlatformApi.V1.Models.Page.Res;

namespace LearningPlatformApi.Services;

public interface IPageService
{
    Task<V1PageResDto> CreateAsync(CreatePageRequest request, User user, CancellationToken cancellationToken);
    Task<V1PageResDto> UpdateAsync(string id, User user, UpdatePageRequest request, CancellationToken cancellationToken);
    Task<V1PageResDto> GetLatestAsync(string id, CancellationToken cancellationToken);
    Task<V1PageResDto> GetByVersionAsync(string id, int versionOrder, CancellationToken cancellationToken);
    Task DeleteAsync(string id, User user, CancellationToken cancellationToken);
    Task<V1PageResDto> RestoreAsync(string id, CancellationToken cancellationToken);
    
    Task<V1PageResDto> RollbackToVersionAsync(string id, int targetVersionOrder, string? reason, CancellationToken cancellationToken);
    Task<List<PageVersionInfoDto>> GetVersionHistoryAsync(string id, int limit, CancellationToken cancellationToken);
    Task<PageComparisonResDto> CompareVersionsAsync(string id, int sourceVersion, int targetVersion, CancellationToken cancellationToken);
    
    Task<V1PageResDto> CopyPageAsync(CopyPageRequest request, User user, CancellationToken cancellationToken);
    
    Task<List<V1PageResDto>> GetPagesByIdsAsync(List<string> ids, CancellationToken cancellationToken);
    Task<V1PageResDto> ReorderContentBlocksAsync(string id, List<int> newOrders, CancellationToken cancellationToken);
}