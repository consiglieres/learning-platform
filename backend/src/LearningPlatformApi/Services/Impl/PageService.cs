using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Domain.Repositories;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Persistence.Entities.Page;
using LearningPlatformApi.Persistence.Repositories.Base;
using LearningPlatformApi.V1.Mapper;
using LearningPlatformApi.V1.Models.Base;
using LearningPlatformApi.V1.Models.Page;
using LearningPlatformApi.V1.Models.Page.Req;
using LearningPlatformApi.V1.Models.Page.Res;

namespace LearningPlatformApi.Services.Impl;

public class PageService(
    IV1ResDtoMapper resDtoMapper,
    IPageRepository pageRepository,
    IDbEntityMapper<Page, string, PageEntity, string> pageEntityMapper,
    IUnitOfWork unitOfWork) : IPageService
{
    public async Task<V1PageResDto> CreateAsync(CreatePageRequest request, User user,
        CancellationToken cancellationToken)
    {
        var page = new Page(Guid.NewGuid().ToString(), request.Order, request.Type);
        var contentBlocks = request.ContentBlocks.Select(x =>
            {
                var contentBlock = new PageContentBlock(Guid.NewGuid().ToString(), page.Id, x.Order, x.Type, x.Data);
                contentBlock.MarkAsCreated(user, DateTimeOffset.UtcNow);
                return contentBlock;
            })
            .ToList();
        page.MarkAsCreated(user, DateTimeOffset.UtcNow);
        page.ContentBlocks = contentBlocks;

        await pageRepository.CreateAsync(page, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        var createdPage = await pageRepository.GetLastAsync(page.Id, cancellationToken);

        return resDtoMapper.Map(createdPage);
    }

    public async Task<V1PageResDto> UpdateAsync(string id, User user, UpdatePageRequest request,
        CancellationToken cancellationToken)
    {
        var page = await pageRepository.GetLastAsync(id, cancellationToken);
        page.Order = request.Order;
        page.Type = request.Type;

        var existingBlocksDict = page.ContentBlocks
            .ToDictionary(b => b.Id, b => b);

        var updatedBlocks = new List<PageContentBlock>();
        var processedBlockIds = new HashSet<string>();

        foreach (var blockRequest in request.ContentBlocks)
        {
            PageContentBlock contentBlock;

            if (!string.IsNullOrEmpty(blockRequest.Id) &&
                existingBlocksDict.TryGetValue(blockRequest.Id, out var existingBlock))
            {
                contentBlock = existingBlock with
                {
                    Order = blockRequest.Order,
                    Type = blockRequest.Type,
                    Data = blockRequest.Data
                };
                contentBlock.MarkAsUpdated(user, DateTimeOffset.UtcNow);
                processedBlockIds.Add(blockRequest.Id);
            }
            else
            {
                contentBlock = new PageContentBlock(
                    blockRequest.Id ?? Guid.NewGuid().ToString(),
                    page.Id,
                    blockRequest.Order,
                    blockRequest.Type,
                    blockRequest.Data);
                contentBlock.MarkAsCreated(user, DateTimeOffset.UtcNow);
            }

            updatedBlocks.Add(contentBlock);
        }

        var unchangedBlocks = page.ContentBlocks
            .Where(b => !processedBlockIds.Contains(b.Id))
            .ToList();

        updatedBlocks.AddRange(unchangedBlocks);

        page.ContentBlocks = updatedBlocks;

        var updated = await pageRepository.UpdateAsync(page, cancellationToken);

        return resDtoMapper.Map(updated);
    }

    public async Task<V1PageResDto> GetLatestAsync(string id, CancellationToken cancellationToken)
    {
        var page = await pageRepository.GetLastAsync(id, cancellationToken);

        return resDtoMapper.Map(page);
    }

    public async Task<V1PageResDto> GetByVersionAsync(string id, int versionOrder, CancellationToken cancellationToken)
    {
        var page = await pageRepository.GetAsync(id, new EntityVersion(versionOrder), cancellationToken);

        return resDtoMapper.Map(page);
    }

    public async Task DeleteAsync(string id, User user, CancellationToken cancellationToken)
    {
        await pageRepository.DeleteAsync(id, user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<V1PageResDto> RestoreAsync(string id, User user, CancellationToken cancellationToken)
    {
        var page = await pageRepository.GetLastAsync(id, cancellationToken);
        page.Restore(user, DateTimeOffset.UtcNow);
        var updated = await pageRepository.UpdateAsync(page, cancellationToken);

        return resDtoMapper.Map(updated);
    }

    public async Task<V1PageResDto> RollbackToVersionAsync(string id, int targetVersionOrder, string? reason,
        CancellationToken cancellationToken)
    {
        var targetPage = await pageRepository.GetAsync(id, new EntityVersion(targetVersionOrder), cancellationToken);

        var currentPage = await pageRepository.GetLastAsync(id, cancellationToken);

        var rolledBackPage = targetPage with
        {
            Version = new EntityVersion(currentPage.Version.Order + 1, Guid.NewGuid().ToString())
        };

        rolledBackPage.ContentBlocks = targetPage.ContentBlocks.Select(block => block with
        {
            Id = Guid.NewGuid().ToString(),
            PageId = rolledBackPage.Id
        }).ToList();
        rolledBackPage.MarkAsCreated(currentPage.CreatedBy, DateTimeOffset.UtcNow);

        await pageRepository.CreateAsync(rolledBackPage, cancellationToken);

        return resDtoMapper.Map(rolledBackPage);
    }

    public async Task<List<PageVersionInfoDto>> GetVersionHistoryAsync(string id, int limit,
        CancellationToken cancellationToken)
    {
        var allPages = await pageRepository.GetAllVersionsAsync(id, cancellationToken);

        var history = allPages
            .OrderByDescending(p => p.Version.Order)
            .Take(limit)
            .Select(version => new PageVersionInfoDto
            {
                Version = new VersionDto
                {
                    Order = version.Version.Order,
                    Tag = version.Version.Tag
                },
                CreatedAt = version.CreatedAt,
                CreatedBy = resDtoMapper.Map(version.CreatedBy),
                ContentBlocksCount = version.ContentBlocks.Count,
                ChangeDescription = GetChangeDescription(version, allPages)
            })
            .ToList();

        return history;
    }

    public async Task<PageComparisonResDto> CompareVersionsAsync(string id, int sourceVersion, int targetVersion,
        CancellationToken cancellationToken)
    {
        var sourcePage = await pageRepository.GetAsync(id, new EntityVersion(sourceVersion), cancellationToken);
        var targetPage = await pageRepository.GetAsync(id, new EntityVersion(targetVersion), cancellationToken);

        var sourceBlocks = sourcePage.ContentBlocks.ToDictionary(b => b.Order, b => b);
        var targetBlocks = targetPage.ContentBlocks.ToDictionary(b => b.Order, b => b);

        var addedBlocks = new List<BlockDiffDto>();
        var removedBlocks = new List<BlockDiffDto>();
        var modifiedBlocks = new List<BlockDiffDto>();

        foreach (var (order, targetBlock) in targetBlocks)
            if (sourceBlocks.TryGetValue(order, out var sourceBlock))
            {
                if (sourceBlock.Data != targetBlock.Data || sourceBlock.Type != targetBlock.Type)
                    modifiedBlocks.Add(new BlockDiffDto
                    {
                        Order = order,
                        Type = targetBlock.Type,
                        OldData = sourceBlock.Data,
                        NewData = targetBlock.Data
                    });
            }
            else
            {
                addedBlocks.Add(new BlockDiffDto
                {
                    Order = order,
                    Type = targetBlock.Type,
                    NewData = targetBlock.Data
                });
            }

        foreach (var (order, sourceBlock) in sourceBlocks)
            if (!targetBlocks.ContainsKey(order))
                removedBlocks.Add(new BlockDiffDto
                {
                    Order = order,
                    Type = sourceBlock.Type,
                    OldData = sourceBlock.Data
                });

        return new PageComparisonResDto
        {
            SourceVersion = resDtoMapper.Map(sourcePage),
            TargetVersion = resDtoMapper.Map(targetPage),
            Differences = new PageDiffDto
            {
                AddedBlocks = addedBlocks,
                RemovedBlocks = removedBlocks,
                ModifiedBlocks = modifiedBlocks
            }
        };
    }

    public async Task<V1PageResDto> CopyPageAsync(CopyPageRequest request, User user,
        CancellationToken cancellationToken)
    {
        // Получаем исходную страницу
        Page sourcePage;
        if (request.SourceVersionOrder.HasValue)
            sourcePage = await pageRepository.GetAsync(
                request.SourcePageId,
                new EntityVersion(request.SourceVersionOrder.Value),
                cancellationToken);
        else
            sourcePage = await pageRepository.GetLastAsync(request.SourcePageId, cancellationToken);

        var newPage = sourcePage with
        {
            Id = Guid.NewGuid().ToString(),
            Order = request.NewOrder,
            Version = EntityVersion.CreateDefault()
        };

        newPage.ContentBlocks = sourcePage.ContentBlocks.Select(block => new PageContentBlock(
            Guid.NewGuid().ToString(),
            newPage.Id,
            block.Order,
            block.Type,
            block.Data
        )).ToList();

        newPage.MarkAsCreated(user, DateTimeOffset.UtcNow);

        // Сохраняем
        await pageRepository.UpdateAsync(newPage, cancellationToken);

        return resDtoMapper.Map(newPage);
    }

    public async Task<List<V1PageResDto>> GetPagesByIdsAsync(List<string> ids, CancellationToken cancellationToken)
    {
        var pages = new List<V1PageResDto>();

        foreach (var id in ids)
            try
            {
                var page = await pageRepository.GetLastAsync(id, cancellationToken);
                pages.Add(resDtoMapper.Map(page));
            }
            catch (DomainException)
            {
                // Страница не найдена - пропускаем
            }

        return pages;
    }

    public async Task<V1PageResDto> ReorderContentBlocksAsync(string id, List<int> newOrders,
        CancellationToken cancellationToken)
    {
        var page = await pageRepository.GetLastAsync(id, cancellationToken);

        if (page.ContentBlocks.Count != newOrders.Count)
            throw new DomainException("Number of orders doesn't match number of content blocks");

        var blocksByOrder = page.ContentBlocks.ToDictionary(b => b.Order, b => b);
        var updatedBlocks = new List<PageContentBlock>();

        for (var i = 0; i < newOrders.Count; i++)
        {
            var oldOrder = newOrders[i];
            if (!blocksByOrder.TryGetValue(oldOrder, out var block))
                throw new DomainException($"Block with order {oldOrder} not found");

            var updatedBlock = block with { Order = i + 1 };
            updatedBlocks.Add(updatedBlock);
        }

        page.ContentBlocks = updatedBlocks.OrderBy(b => b.Order).ToList();

        var updated = await pageRepository.UpdateAsync(page, cancellationToken);

        return resDtoMapper.Map(updated);
    }

    private string GetChangeDescription(Page page, IReadOnlyCollection<Page> allVersions)
    {
        var previousVersion = allVersions
            .FirstOrDefault(p => p.Version.Order == page.Version.Order - 1);

        if (previousVersion == null) return "Initial version";

        var changes = new List<string>();

        if (page.Order != previousVersion.Order)
            changes.Add($"Order changed from {previousVersion.Order} to {page.Order}");

        if (page.Type != previousVersion.Type)
            changes.Add($"Type changed from {previousVersion.Type} to {page.Type}");

        var blocksAdded = page.ContentBlocks.Count - previousVersion.ContentBlocks.Count;
        if (blocksAdded > 0)
            changes.Add($"{blocksAdded} block(s) added");
        else if (blocksAdded < 0)
            changes.Add($"{Math.Abs(blocksAdded)} block(s) removed");

        var modifiedBlocks = page.ContentBlocks
            .Count(b => previousVersion.ContentBlocks.Any(pb => pb.Order == b.Order && pb.Data != b.Data));

        if (modifiedBlocks > 0)
            changes.Add($"{modifiedBlocks} block(s) modified");

        return changes.Any() ? string.Join(", ", changes) : "No significant changes";
    }

    private async Task<Page?> GetPreviousVersionAsync(string id, int currentVersionOrder,
        CancellationToken cancellationToken)
    {
        try
        {
            return await pageRepository.GetAsync(id, new EntityVersion(currentVersionOrder - 1), cancellationToken);
        }
        catch (DomainException)
        {
            return null;
        }
    }
}