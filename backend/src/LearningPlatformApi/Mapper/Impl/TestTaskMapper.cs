using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.Entities.Tasks;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.Domain.ValueObjects.Task;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Entities.Page;
using Riok.Mapperly.Abstractions;

namespace LearningPlatformApi.Mapper.Impl;

[Mapper]
internal partial class TestTaskMapper(
    IDbEntityMapper<Page, string, PageEntity, string> pageMapper,
    IUserMapper userMapper) : IDbEntityMapper<TestTask, string, TestTaskEntity, string>
{
    public TestTask Map(TestTaskEntity entity)
    {
        return new TestTask(entity.Name, entity.Order,
            new Difficulty(entity.DifficultyCategory, entity.DifficultyPoints),
            entity.LessonId, entity.Page == null ? null : pageMapper.Map(entity.Page), entity.Question, entity.Options,
            entity.CorrectAnswer, userMapper.MapToDomain(entity.CreatedByUser))
        {
            Id = entity.Id,
            Version = new EntityVersion(entity.VersionOrder, entity.Tag),
            CreatedAt = entity.CreatedAt,
            CreatedBy = userMapper.MapToDomain(entity.CreatedByUser),
            UpdatedAt = entity.UpdatedAt,
            UpdatedBy = entity.UpdatedByUser == null ? null : userMapper.MapToDomain(entity.UpdatedByUser),
            DeletedAt = entity.DeletedAt,
            DeletedBy = entity.DeletedByUser == null ? null : userMapper.MapToDomain(entity.DeletedByUser)
        };
    }

    public TestTaskEntity Map(TestTask entity)
    {
        return new TestTaskEntity(entity.Id)
        {
            Question = entity.Question,
            Options = entity.Options.ToList(),
            CorrectAnswer = entity.Answer,
            Name = entity.Name,
            Order = entity.Order,
            DifficultyCategory = entity.Difficulty.Name,
            DifficultyPoints = entity.Difficulty.BasePoints,
            LessonId = entity.LessonId,
            PageId = entity.PageContent.Id,
            Page = pageMapper.Map(entity.PageContent),
            VersionOrder = entity.Version.Order,
            Tag = entity.Version.Tag,
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