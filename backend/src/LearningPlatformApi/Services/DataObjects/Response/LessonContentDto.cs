namespace LearningPlatformApi.Services.DataObjects.Response;

public record LessonContentDto(
    string Id,
    string Name,
    PageContentDto? Theory,
    List<TaskPreviewDto> Tasks,
    LessonProgressDto? Progress);