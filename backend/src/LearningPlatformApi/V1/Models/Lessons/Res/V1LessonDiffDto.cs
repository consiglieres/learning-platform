namespace LearningPlatformApi.V1.Models.Lessons.Res;

public class LessonDiffDto
{
    public required string LessonId { get; set; }

    public required string LessonName { get; set; }

    public int LessonOrder { get; set; }

    public int PassThreshold { get; set; }

    public string? OldContent { get; set; }

    public string? NewContent { get; set; }
}