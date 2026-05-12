using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Domain.Repositories;
using LearningPlatformApi.Domain.ValueObjects.Page;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Repositories.Base;
using LearningPlatformApi.V1.Mapper;
using LearningPlatformApi.V1.Models.Lessons.Req;
using LearningPlatformApi.V1.Models.Lessons.Res;

namespace LearningPlatformApi.Services.Impl;

public class LessonService(ILessonRepository lessonRepository, IV1ResDtoMapper resDtoMapper, IUnitOfWork unitOfWork)
    : ILessonService
{
    public async Task<V1LessonResDto> CreateAsync(V1CreateLessonReqDto request, User user, CancellationToken cancellationToken)
    {
        var newLesson = new Lesson(request.Name, request.LessonOrder, request.PassThreshold,
            Page.EmptyPage(PageType.Theory, user), request.ModuleId, user);

        await lessonRepository.CreateAsync(newLesson, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        var created = await lessonRepository.GetByIdAsync(newLesson.Id, cancellationToken);

        return resDtoMapper.Map(created);
    }

    public async Task<V1LessonResDto> UpdateAsync(string id, User user, V1UpdateLessonReqDto request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var existingLesson = await lessonRepository.GetByIdAsync(id, cancellationToken);
            if (existingLesson == null)
                throw new DomainException("Lesson not found");

            existingLesson.Name = request.Name;
            existingLesson.LessonOrder = request.LessonOrder;
            existingLesson.PassThreshold = request.PassThreshold;
            await lessonRepository.UpdateAsync(existingLesson, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
            var upadted = await lessonRepository.GetByIdAsync(id, cancellationToken);

            return resDtoMapper.Map(upadted);
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task<V1LessonResDto> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var lesson = await lessonRepository.GetByIdAsync(id, cancellationToken);
        return resDtoMapper.Map(lesson);
    }

    public async Task DeleteAsync(string id, User user, CancellationToken cancellationToken)
    {
        await lessonRepository.DeleteAsync(id, user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<V1LessonResDto> RestoreAsync(string id, User user, CancellationToken cancellationToken)
    {
        var lesson = await lessonRepository.GetByIdAsync(id, cancellationToken);

        lesson.Restore(user, DateTimeOffset.UtcNow);

        await lessonRepository.CreateAsync(lesson, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var updated = await lessonRepository.GetByIdAsync(lesson.Id, cancellationToken);
        return resDtoMapper.Map(updated);
    }

    public async Task<List<V1LessonResDto>> GetLessonsByIdsAsync(List<string> ids, CancellationToken cancellationToken)
    {
        var lessons = await lessonRepository.GetByIdsAsync(ids, cancellationToken);

        return lessons.Select(resDtoMapper.Map).ToList();
    }

    public async Task<IReadOnlyCollection<V1LessonResDto>> ReorderLessonsAsync(string id, List<string> lessonIds, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var lessons = await lessonRepository.GetByIdsAsync(lessonIds, cancellationToken);
            var lessonsById = lessons.ToDictionary(x => x.Id);

            for (var i = 1; i < lessonIds.Count + 1; i++)
            {
                var lessonId = lessonIds[i];
                if (!lessonsById.TryGetValue(lessonId, out var lesson))
                {
                    throw new DomainException("Reorder lesson id not found");
                }

                lesson.LessonOrder = i;
                await lessonRepository.CreateAsync(lesson, cancellationToken);
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);

            var updatedLessons = await lessonRepository.GetByIdsAsync(lessonIds, cancellationToken);
            return updatedLessons.Select(resDtoMapper.Map).ToList();
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}