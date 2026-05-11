using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.Entities.Tasks;
using LearningPlatformApi.Domain.ValueObjects.Task;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Entities.Page;
using Riok.Mapperly.Abstractions;

namespace LearningPlatformApi.Mapper.Impl;

[Mapper]
internal partial class CodingTaskMapper(
    IDbEntityMapper<Page, string, PageEntity, string> pageMapper,
    IUserMapper userMapper)
    : IDbEntityMapper<CodingTask, string, CodingTaskEntity, string>
{
    public CodingTask Map(CodingTaskEntity entity)
    {
        return new CodingTask(entity.Name, entity.Order,
            new Difficulty(entity.DifficultyCategory, entity.DifficultyPoints),
            entity.LessonId,
            pageMapper.Map(entity.Page),
            entity.InitialCode, entity.TestCode, userMapper.MapToDomain(entity.CreatedByUser))
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            CreatedBy = userMapper.MapToDomain(entity.CreatedByUser),
            UpdatedAt = entity.UpdatedAt,
            UpdatedBy = entity.UpdatedByUser == null ? null : userMapper.MapToDomain(entity.UpdatedByUser),
            DeletedAt = entity.DeletedAt,
            DeletedBy = entity.DeletedByUser == null ? null : userMapper.MapToDomain(entity.DeletedByUser)
        };
    }

    public CodingTaskEntity Map(CodingTask entity)
    {
        return new CodingTaskEntity(entity.Id)
        {
            InitialCode = entity.InitialCode,
            TestCode = entity.TestCode,
            Name = entity.Name,
            Order = entity.Order,
            DifficultyCategory = entity.Difficulty.Name,
            DifficultyPoints = entity.Difficulty.BasePoints,
            LessonId = entity.LessonId,
            PageId = entity.PageContent.Id,
            Page = pageMapper.Map(entity.PageContent),
            CreatedAt = entity.CreatedAt,
            CreatedBy = entity.CreatedBy.Id,
            CreatedByUser = userMapper.MapToEntity(entity.CreatedBy),
            UpdatedAt = entity.UpdatedAt,
            UpdatedBy = entity.UpdatedBy?.Id,
            UpdatedByUser = entity.UpdatedBy == null ? null : userMapper.MapToEntity(entity.UpdatedBy),
            DeletedAt = entity.DeletedAt,
            DeletedBy = entity.DeletedBy?.Id,
            DeletedByUser = entity.DeletedBy == null ? null : userMapper.MapToEntity(entity.DeletedBy)
        };
    }

    public string MapId(string id)
    {
        return id;
    }
}