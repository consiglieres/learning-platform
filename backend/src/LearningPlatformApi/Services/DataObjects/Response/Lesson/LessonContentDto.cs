using LearningPlatformApi.Services.DataObjects.Response.Page;
using LearningPlatformApi.Services.DataObjects.Response.Task;

namespace LearningPlatformApi.Services.DataObjects.Response.Lesson;

public record LessonContentDto(
    string Id,
    string Name,
    PageContentDto? Theory,
    List<TaskPreviewDto> Tasks,
    LessonProgressDto? Progress);