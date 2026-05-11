using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Domain.Repositories;
using LearningPlatformApi.Persistence.Repositories.Base;
using LearningPlatformApi.V1.Mapper;
using LearningPlatformApi.V1.Models.Page;
using LearningPlatformApi.V1.Models.Page.Req;

namespace LearningPlatformApi.Services.Impl;

public class PageService(IV1ResDtoMapper resDtoMapper, IPageRepository pageRepository, IUnitOfWork unitOfWork)
    : IPageService
{
    public async Task<V1PageResDto> CreateAsync(CreatePageRequest request, User user,
        CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var page = new Page(Guid.NewGuid().ToString(), request.Order, request.Type);
            var contentBlocks = request.ContentBlocks.Select(x =>
                {
                    var contentBlock =
                        new PageContentBlock(Guid.NewGuid().ToString(), page.Id, x.Order, x.Type, x.Data);
                    contentBlock.MarkAsCreated(user, DateTimeOffset.UtcNow);
                    return contentBlock;
                })
                .ToList();
            page.MarkAsCreated(user, DateTimeOffset.UtcNow);
            page.ContentBlocks = contentBlocks;
            await pageRepository.CreateAsync(page, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransactionAsync(cancellationToken);
            var createdPage = await pageRepository.GetByIdAsync(page.Id, cancellationToken);

            return resDtoMapper.Map(createdPage);
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task<V1PageResDto> UpdateAsync(string id, User user, UpdatePageRequest request,
        CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var page = await pageRepository.GetByIdAsync(id, cancellationToken);
            var newPage = new Page(id, request.Order, request.Type);
            var contentBlocks = request.ContentBlocks.Select(x =>
                {
                    var contentBlock = new PageContentBlock(Guid.NewGuid().ToString(), page.Id, x.Order, x.Type, x.Data);
                    contentBlock.MarkAsCreated(user, DateTimeOffset.UtcNow);
                    return contentBlock;
                })
                .ToList();
            newPage.MarkAsCreated(user, DateTimeOffset.UtcNow);
            newPage.ContentBlocks = contentBlocks;
            await pageRepository.CreateAsync(newPage, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransactionAsync(cancellationToken);
            var createdPage = await pageRepository.GetByIdAsync(newPage.Id, cancellationToken);
            return resDtoMapper.Map(createdPage);
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task<V1PageResDto> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var page = await pageRepository.GetByIdAsync(id, cancellationToken);

        return resDtoMapper.Map(page);
    }

    public async Task DeleteAsync(string id, User user, CancellationToken cancellationToken)
    {
        await pageRepository.DeleteAsync(id, user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<V1PageResDto> RestoreAsync(string id, User user, CancellationToken cancellationToken)
    {
        try
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);
            var deletedPage = await pageRepository.GetByIdAsync(id, cancellationToken);

            if (deletedPage == null || deletedPage.DeletedAt == null)
                throw new DomainException("No deleted version found");

            var restoredPage = new Page(id, deletedPage.Order, deletedPage.Type)
            {
                ContentBlocks = deletedPage.ContentBlocks.Select(block => new PageContentBlock(
                    Guid.NewGuid().ToString(),
                    id,
                    block.Order,
                    block.Type,
                    block.Data
                )).ToList(),
            };

            restoredPage.MarkAsCreated(user, DateTimeOffset.UtcNow);
            restoredPage.MarkAsUpdated(user, DateTimeOffset.UtcNow);

            await pageRepository.CreateAsync(restoredPage, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return resDtoMapper.Map(restoredPage);
        }
        catch (Exception)
        {
            await unitOfWork.CommitTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task<List<V1PageResDto>> GetPagesByIdsAsync(List<string> ids, CancellationToken cancellationToken)
    {
        var pages = new List<V1PageResDto>();

        foreach (var id in ids)
            try
            {
                var page = await pageRepository.GetByIdAsync(id, cancellationToken);
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
        var page = await pageRepository.GetByIdAsync(id, cancellationToken);

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
}