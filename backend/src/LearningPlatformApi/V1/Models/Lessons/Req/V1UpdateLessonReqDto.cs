namespace LearningPlatformApi.V1.Models.Lessons.Req;

public class V1UpdateLessonReqDto
{
    public required string Name { get; set; }

    public int LessonOrder { get; set; }

    public int PassThreshold { get; set; }
}